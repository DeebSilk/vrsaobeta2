using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerPCController : MonoBehaviour
{
	[Header("Links")]
	[SerializeField] Transform cameraPivot;

	[Header("Move")]
	[SerializeField] float walkSpeed = 4.5f;
	[SerializeField] float sprintMultiplier = 1.5f;
	[SerializeField] float gravity = -9.81f;

	[Header("Look")]
	[SerializeField] float mouseSensitivity = 0.12f; // 0.08Ц0.2
	[SerializeField] float minPitch = -60f;
	[SerializeField] float maxPitch = 75f;
	[SerializeField] float lookSmooth = 25f; // сглаживание

	CharacterController cc;

	Vector2 moveInput;
	Vector2 lookRaw;
	Vector2 lookSmoothed;
	bool sprintHeld;

	Vector3 velocity;
	float yaw;
	float pitch;

	void Awake()
	{
		cc = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		yaw = transform.eulerAngles.y;
	}

	void Update()
	{
		UpdateLook();
		UpdateMove();
	}

	public void OnMove(InputValue v) => moveInput = v.Get<Vector2>();
	public void OnLook(InputValue v) => lookRaw = v.Get<Vector2>();
	public void OnSprint(InputValue v) => sprintHeld = v.isPressed;

	void UpdateLook()
	{
		// сглаживаем мышь, чтобы не дЄргалось
		float t = 1f - Mathf.Exp(-lookSmooth * Time.unscaledDeltaTime);
		lookSmoothed = Vector2.Lerp(lookSmoothed, lookRaw, t);

		yaw += lookSmoothed.x * mouseSensitivity;
		pitch = Mathf.Clamp(pitch - lookSmoothed.y * mouseSensitivity, minPitch, maxPitch);

		// тело крутитс€ всегда (yaw)
		transform.rotation = Quaternion.Euler(0f, yaw, 0f);

		// камера вверх/вниз (pitch)
		if (cameraPivot)
			cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
	}

	void UpdateMove()
	{
		Vector3 input = new(moveInput.x, 0f, moveInput.y);
		if (input.sqrMagnitude > 1f)
			input.Normalize();

		float speed = sprintHeld ? walkSpeed * sprintMultiplier : walkSpeed;
		Vector3 moveWorld = transform.TransformDirection(input) * speed;
		cc.Move(moveWorld * Time.deltaTime);

		if (cc.isGrounded && velocity.y < 0f)
			velocity.y = -2f;
		velocity.y += gravity * Time.deltaTime;
		cc.Move(velocity * Time.deltaTime);
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
	public static HUDManager Instance { get; private set; }

	[Header("UI References")]
	[SerializeField] private Canvas hudCanvas;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private Animator animator;
	[SerializeField] private Image hpFillImage;

	[Header("Find Targets By Tag")]
	[SerializeField] private string playerTag = "Player";
	[SerializeField] private string headTag = "MainCamera"; // VR "голова" обычно камера

	[Header("Follow Settings (World Space HUD)")]
	[SerializeField] private bool follow = true;
	[SerializeField] private Vector3 localOffset = new Vector3(0f, -0.15f, 0.75f);
	[SerializeField] private float followSmooth = 12f;
	[SerializeField] private bool faceCamera = true;

	[Header("Sorting (HUD visible over game)")]
	[SerializeField] private int hudSortingOrder = 100;

	private Transform followTarget;
	private Camera mainCam;

	private float targetAlpha = 1f;

	private void Awake()
	{
		// Singleton
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		// Auto-grab refs if empty
		if (!hudCanvas)
			hudCanvas = GetComponentInChildren<Canvas>(true);
		if (!canvasGroup)
			canvasGroup = GetComponentInChildren<CanvasGroup>(true);
		if (!animator)
			animator = GetComponentInChildren<Animator>(true);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start()
	{
		ApplyCanvasSettings();
		RebindToScene();          // найдет камеру/игрока
		SetVisible(true, true);   // сразу видно
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		if (Instance == this)
			Instance = null;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		RebindToScene();
	}

	private void Update()
	{
		// Плавная прозрачность (работает даже без аниматора)
		if (canvasGroup)
		{
			canvasGroup.alpha = Mathf.MoveTowards(
				canvasGroup.alpha,
				targetAlpha,
				Time.unscaledDeltaTime * 6f
			);
		}

		if (follow && followTarget)
			FollowTick();
	}

	private void ApplyCanvasSettings()
	{
		if (!hudCanvas)
			return;

		// HUD поверх обычного UI (если в сцене есть другие Canvas)
		hudCanvas.overrideSorting = true;
		hudCanvas.sortingOrder = hudSortingOrder;
	}

	public void RebindToScene()
	{
		mainCam = Camera.main;

		// 1) Сначала пробуем найти голову (камера с тегом MainCamera)
		var headObj = GameObject.FindGameObjectWithTag(headTag);
		if (headObj != null)
			followTarget = headObj.transform;

		// 2) Если камеры нет/тег не стоит — ищем игрока
		if (!followTarget)
		{
			var playerObj = GameObject.FindGameObjectWithTag(playerTag);
			if (playerObj != null)
				followTarget = playerObj.transform;
		}

		// 3) Event Camera для World Space UI
		if (hudCanvas && mainCam)
			hudCanvas.worldCamera = mainCam;

		// 4) Ставим HUD сразу в корректное место (без рывка)
		if (followTarget)
		{
			transform.position = followTarget.TransformPoint(localOffset);

			if (mainCam && faceCamera)
			{
				Vector3 toCam = (mainCam.transform.position - transform.position).normalized;
				transform.rotation = Quaternion.LookRotation(-toCam, Vector3.up);
			}
		}
	}

	private void FollowTick()
	{
		if (!mainCam)
			mainCam = Camera.main;
		if (!mainCam)
			return;

		Vector3 desiredPos = followTarget.TransformPoint(localOffset);
		transform.position = Vector3.Lerp(
			transform.position,
			desiredPos,
			Time.unscaledDeltaTime * followSmooth
		);

		if (faceCamera)
		{
			Vector3 toCam = (mainCam.transform.position - transform.position).normalized;
			Quaternion desiredRot = Quaternion.LookRotation(-toCam, Vector3.up);

			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				desiredRot,
				Time.unscaledDeltaTime * followSmooth
			);
		}
	}

	// ==========================
	// HP API (Fill Amount)
	// ==========================
	public void SetHP01(float value01)
	{
		if (!hpFillImage)
			return;
		hpFillImage.fillAmount = Mathf.Clamp01(value01);
	}

	// ==========================
	// Visibility + Anime Animations
	// ==========================
	public void SetVisible(bool visible, bool instant = false)
	{
		targetAlpha = visible ? 1f : 0f;

		if (canvasGroup)
		{
			canvasGroup.interactable = visible;
			canvasGroup.blocksRaycasts = visible;

			if (instant)
				canvasGroup.alpha = targetAlpha;
		}

		// Если есть Animator — он отработает “как в аниме”
		if (animator)
		{
			animator.ResetTrigger("Show");
			animator.ResetTrigger("Hide");
			animator.SetTrigger(visible ? "Show" : "Hide");
		}
	}
}

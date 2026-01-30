using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CenterAimUIMouse : MonoBehaviour
{
	[Header("ПК прицел по центру экрана")]
	public bool enable = true;          // ← ВАЖНО
	public Camera cam;
	public KeyCode clickKey = KeyCode.E;

	private PointerEventData pointerData;
	private GameObject currentHover;
	private readonly List<RaycastResult> results = new();

	void Awake()
	{
		if (!cam)
			cam = Camera.main;
	}

	void Update()
	{
		if (!enable)
			return;             // ← ВАЖНО
		if (!EventSystem.current || !cam)
			return;

		if (pointerData == null)
			pointerData = new PointerEventData(EventSystem.current);

		pointerData.position = new Vector2(
			Screen.width * 0.5f,
			Screen.height * 0.5f
		);

		results.Clear();
		EventSystem.current.RaycastAll(pointerData, results);

		GameObject hit = results.Count > 0 ? results[0].gameObject : null;

		// Hover
		if (hit != currentHover)
		{
			if (currentHover)
				ExecuteEvents.Execute(currentHover, pointerData, ExecuteEvents.pointerExitHandler);

			currentHover = hit;

			if (currentHover)
				ExecuteEvents.Execute(currentHover, pointerData, ExecuteEvents.pointerEnterHandler);
		}

		// Click
		if (currentHover && Input.GetKeyDown(clickKey))
		{
			ExecuteEvents.Execute(currentHover, pointerData, ExecuteEvents.pointerDownHandler);
			ExecuteEvents.Execute(currentHover, pointerData, ExecuteEvents.pointerClickHandler);
			ExecuteEvents.Execute(currentHover, pointerData, ExecuteEvents.pointerUpHandler);
		}
	}
}

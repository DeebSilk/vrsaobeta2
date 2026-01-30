using UnityEngine;

public class EscMenuToggle : MonoBehaviour
{
	public MenuFocusController focus;
	private bool isOpen;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isOpen = !isOpen;

			if (HUDManager.Instance != null)
				HUDManager.Instance.SetVisible(isOpen);

			if (focus)
			{
				if (isOpen)
					focus.OnMenuOpened();
				else
					focus.OnMenuClosed();
			}
		}
	}
}

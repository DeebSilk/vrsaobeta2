using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFocusController : MonoBehaviour
{
	[Header("Первая кнопка/элемент меню (Select on open)")]
	public GameObject firstSelected;

	GameObject lastSelected;

	public void OnMenuOpened()
	{
		if (!EventSystem.current)
			return;

		// запоминаем что было выбрано до открытия
		lastSelected = EventSystem.current.currentSelectedGameObject;

		// ставим фокус на первую кнопку
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(firstSelected);
	}

	public void OnMenuClosed()
	{
		if (!EventSystem.current)
			return;

		// очистить выделение (чтобы не было случайных submit)
		EventSystem.current.SetSelectedGameObject(null);
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
	// Ќазвани€ сцен (должны совпадать 1:1 с именами в Project)
	public const string MainMenu = "MainMenu";
	public const string HubCity = "Hub_City";
	public const string Forest = "Zone_ForestField";

	public static void Load(string sceneName)
	{
		Time.timeScale = 1f;                 // на вс€кий случай снимаем паузу
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		SceneManager.LoadScene(sceneName);
	}

	public static void QuitGame()
	{
		Application.Quit();
	}
}

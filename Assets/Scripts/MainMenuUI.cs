using UnityEngine;

public sealed class MainMenuUI : MonoBehaviour
{
	[Header("UI Panels")]
	[SerializeField] private GameObject mainPanel;      // панель с кнопками (Play/Settings/Quit)
	[SerializeField] private GameObject settingsPanel;  // панель настроек (если есть)

	[Header("Start Scene")]
	[Tooltip("Куда отправляем игрока при нажатии 'Начать'. Обычно Hub_City.")]
	[SerializeField] private string startSceneName = SceneLoader.HubCity;

	private void Awake()
	{
		if (mainPanel)
			mainPanel.SetActive(true);
		if (settingsPanel)
			settingsPanel.SetActive(false);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 1f;
	}

	public void Play()
	{
		SceneLoader.Load(startSceneName);
	}

	public void OpenSettings()
	{
		if (mainPanel)
			mainPanel.SetActive(false);
		if (settingsPanel)
			settingsPanel.SetActive(true);
	}

	public void CloseSettings()
	{
		if (settingsPanel)
			settingsPanel.SetActive(false);
		if (mainPanel)
			mainPanel.SetActive(true);
	}

	public void Quit()
	{
		SceneLoader.QuitGame();
	}
}

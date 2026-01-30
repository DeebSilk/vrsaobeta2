using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject settingsPanel;

	private bool isPaused;

	void Start()
	{
		if (pausePanel)
			pausePanel.SetActive(false);
		if (settingsPanel)
			settingsPanel.SetActive(false);
		Resume();
	}

	void Update()
	{
		// ESC на клавиатуре (Input System)
		if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		if (isPaused)
			Resume();
		else
			Pause();
	}

	public void Pause()
	{
		isPaused = true;
		Time.timeScale = 0f;
		if (pausePanel)
			pausePanel.SetActive(true);
		if (settingsPanel)
			settingsPanel.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Resume()
	{
		isPaused = false;
		Time.timeScale = 1f;
		if (pausePanel)
			pausePanel.SetActive(false);
		if (settingsPanel)
			settingsPanel.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void OpenSettings()
	{
		if (!isPaused)
			Pause();
		if (settingsPanel)
			settingsPanel.SetActive(true);
		if (pausePanel)
			pausePanel.SetActive(false);
	}

	public void BackToPause()
	{
		if (pausePanel)
			pausePanel.SetActive(true);
		if (settingsPanel)
			settingsPanel.SetActive(false);
	}
}

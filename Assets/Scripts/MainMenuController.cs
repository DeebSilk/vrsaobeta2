using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MainMenuController : MonoBehaviour
{
	[Header("Scene names")]
	[SerializeField] string playSceneName = "Hub_City"; // помен€й если у теб€ иначе

	public void Play()
	{
		SceneManager.LoadScene(playSceneName);
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public void OpenSettings()
	{
		// позже сделаем окно настроек (громкость/графика/управление)
		Debug.Log("Settings: TODO");
	}
}

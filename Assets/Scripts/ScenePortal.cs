using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class ScenePortal : MonoBehaviour
{
	[SerializeField] string sceneToLoad;
	[SerializeField] string spawnPointIdInNextScene = "default";

	static string nextSpawnId;

	bool isTeleporting;

	void OnTriggerEnter(Collider other)
	{
		if (isTeleporting)
			return;
		if (!other.CompareTag("Player"))
			return;

		isTeleporting = true;

		nextSpawnId = spawnPointIdInNextScene;
		SceneManager.sceneLoaded += OnSceneLoaded;

		// затемняем экран
		if (FadeController.Instance)
			FadeController.Instance.FadeOut();

		// небольшая пауза перед загрузкой сцены
		Invoke(nameof(LoadScene), 0.5f);
	}

	void LoadScene()
	{
		SceneManager.LoadScene(sceneToLoad);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;

		var player = GameObject.FindGameObjectWithTag("Player");
		if (!player)
			return;

		// ищем точку спавна по id
		var points = Object.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
		foreach (var p in points)
		{
			if (p.id != nextSpawnId)
				continue;

			var cc = player.GetComponent<CharacterController>();
			if (cc)
				cc.enabled = false;

			player.transform.SetPositionAndRotation(
				p.transform.position,
				Quaternion.Euler(0f, p.transform.eulerAngles.y, 0f)
			);

			if (cc)
				cc.enabled = true;
			break;
		}

		// проявляем экран
		if (FadeController.Instance)
			FadeController.Instance.FadeIn();

		isTeleporting = false;
	}
}

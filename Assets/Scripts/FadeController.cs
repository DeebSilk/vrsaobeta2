using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public sealed class FadeController : MonoBehaviour
{
	public static FadeController Instance;

	[SerializeField] Image fadeImage;
	[SerializeField] float fadeSpeed = 1.5f;

	Coroutine fadeRoutine;

	void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void Start()
	{
		SetAlpha(0f);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Когда сцена загрузилась — всегда проявляем
		FadeIn();
	}

	public void FadeOut() => StartFade(1f);
	public void FadeIn() => StartFade(0f);

	void StartFade(float target)
	{
		if (fadeRoutine != null)
			StopCoroutine(fadeRoutine);

		fadeRoutine = StartCoroutine(FadeTo(target));
	}

	IEnumerator FadeTo(float target)
	{
		while (!Mathf.Approximately(fadeImage.color.a, target))
		{
			float a = Mathf.MoveTowards(fadeImage.color.a, target, Time.unscaledDeltaTime * fadeSpeed);
			SetAlpha(a);
			yield return null;
		}
	}

	void SetAlpha(float a)
	{
		var c = fadeImage.color;
		c.a = a;
		fadeImage.color = c;
	}
}

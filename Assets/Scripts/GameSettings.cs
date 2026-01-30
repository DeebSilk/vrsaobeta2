using UnityEngine;

public sealed class GameSettings : MonoBehaviour
{
	public static GameSettings Instance;

	public const string VolumeKey = "opt_volume";
	public const string SensKey = "opt_sens";
	public const string FullKey = "opt_fullscreen";

	public float Volume { get; private set; }
	public float MouseSens { get; private set; }
	public bool Fullscreen { get; private set; }

	void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		Load();
		ApplyAll();
	}

	public void SetVolume(float v)
	{
		Volume = Mathf.Clamp01(v);
		PlayerPrefs.SetFloat(VolumeKey, Volume);
		AudioListener.volume = Volume;
	}

	public void SetMouseSens(float s)
	{
		MouseSens = Mathf.Clamp(s, 0.05f, 2.0f);
		PlayerPrefs.SetFloat(SensKey, MouseSens);
	}

	public void SetFullscreen(bool on)
	{
		Fullscreen = on;
		PlayerPrefs.SetInt(FullKey, on ? 1 : 0);
		Screen.fullScreen = on;
	}

	public void Load()
	{
		Volume = PlayerPrefs.GetFloat(VolumeKey, 0.8f);
		MouseSens = PlayerPrefs.GetFloat(SensKey, 0.12f);
		Fullscreen = PlayerPrefs.GetInt(FullKey, 1) == 1;
	}

	public void ApplyAll()
	{
		AudioListener.volume = Volume;
		Screen.fullScreen = Fullscreen;
	}
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
	public AudioMixer mixer;
	public Slider masterSlider;

	void Start()
	{
		float v;
		if (mixer.GetFloat("MasterVolume", out v))
		{
			masterSlider.value = v;
		}
	}

	public void SetMasterVolume(float volume)
	{
		mixer.SetFloat("MasterVolume", volume);
	}
}

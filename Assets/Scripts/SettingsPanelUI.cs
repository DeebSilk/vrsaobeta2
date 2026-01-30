using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsPanelUI : MonoBehaviour
{
	[SerializeField] Slider volumeSlider;
	[SerializeField] Slider sensSlider;
	[SerializeField] Toggle fullscreenToggle;

	void Start()
	{
		var gs = GameSettings.Instance;

		volumeSlider.SetValueWithoutNotify(gs.Volume);
		sensSlider.SetValueWithoutNotify(gs.MouseSens);
		fullscreenToggle.SetIsOnWithoutNotify(gs.Fullscreen);
	}

	public void OnVolumeChanged(float v) => GameSettings.Instance.SetVolume(v);
	public void OnSensChanged(float s) => GameSettings.Instance.SetMouseSens(s);
	public void OnFullscreenChanged(bool on) => GameSettings.Instance.SetFullscreen(on);
}

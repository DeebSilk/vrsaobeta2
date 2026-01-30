using UnityEngine;
using UnityEngine.XR;

public class AutoPlatformUI : MonoBehaviour
{
	public CenterAimUIMouse pcAim;

	void Update()
	{
		bool isVR = XRSettings.isDeviceActive; // true если XR реально активен
		if (pcAim)
			pcAim.enable = !isVR;
	}
}

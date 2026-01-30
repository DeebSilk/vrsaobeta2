using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGate : MonoBehaviour
{
	public string sceneToLoad = "Zone_ForestField";

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			SceneManager.LoadScene(sceneToLoad);
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class toScene : MonoBehaviour {
	public void to(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}
}
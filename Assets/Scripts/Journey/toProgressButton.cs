using UnityEngine;
using UnityEngine.SceneManagement;

public class toProgressButton : MonoBehaviour {
	public void to(string name) {
		SceneManager.LoadScene("Progress", LoadSceneMode.Additive);
	}
}

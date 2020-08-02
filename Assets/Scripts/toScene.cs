using UnityEngine;
using UnityEngine.SceneManagement;

public class toScene : MonoBehaviour 
{
	// This script can be used for any UI controls that return back to main menu
	public void to(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}
}
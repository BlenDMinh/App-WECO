using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toScene : MonoBehaviour {
	// This script can be used for any UI controls that return back to main menu
	public void to(string name) {
		StartCoroutine(LoadAsynchronously(name));
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
	}

	IEnumerator LoadAsynchronously(string name) {
		AsyncOperation operation = SceneManager.LoadSceneAsync(name);
		//SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
		while(!operation.isDone) {
			Debug.Log(operation.progress);
			yield return null;
        }
		//SceneManager.UnloadSceneAsync("Loading");
    }
}
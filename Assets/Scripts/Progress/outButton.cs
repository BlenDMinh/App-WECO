using UnityEngine;
using UnityEngine.SceneManagement;

public class outButton : MonoBehaviour {
	public GameObject canvas;
	string scenename;
	[SerializeField]
	private float delayTime;
	private float time;

	private void Start() {
		time = 0f;
		scenename = "";
    }
    public void to(string name) {
		Animator animator = canvas.GetComponent<Animator>();
		if (animator != null)
			animator.SetBool("isExit", true);
			
		scenename = name;
	}

    private void Update() {
		if (scenename == "")
			return;
		time += Time.deltaTime;
		if (time < delayTime)
			return;
		SceneManager.UnloadSceneAsync(scenename);
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class exitButton : MonoBehaviour {

    public GameObject canvas;
    [SerializeField]
    private float delayTime;
    public Text status;
    private float time;
    private string scenename;
    private void Start() {
        time = 0f;
        scenename = "";
    }
    public void exit(string name) {
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
        status.text = "exit";
        //SceneManager.UnloadSceneAsync(scenename);
    }
}

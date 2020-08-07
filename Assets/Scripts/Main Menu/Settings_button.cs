using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_button : MonoBehaviour
{
    public Button settings_button;
    public Canvas canvas;
    float scale;
    // Start is called before the first frame update
    void Start() {
        scale = canvas.transform.localScale.x;
        RectTransform r = settings_button.GetComponent<RectTransform>();
        r.transform.position = new Vector2(r.transform.position.x, -2 * scale);
    }
    public void to(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}

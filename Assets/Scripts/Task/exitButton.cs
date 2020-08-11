using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitButton : MonoBehaviour {
    public void exit() {
        SceneManager.UnloadSceneAsync("Task");
    }
}

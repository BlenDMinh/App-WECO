using UnityEngine;
using UnityEngine.SceneManagement;

public class journeyAndroidEventControl : MonoBehaviour {

    private Vector2 touchBeginPosition, touchEndPosition;

    private bool ifSceneIsActive(string sceneName) {
        for(int i = 0; i < SceneManager.sceneCount; i++) {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
                return true;
        }
        return false;
    }
    void Update() {
        //Back Button
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (!ifSceneIsActive("Task"))
                    SceneManager.LoadScene("Challenge Board");
                else
                    SceneManager.UnloadSceneAsync("Task");
            }   

        //Swipe
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            touchBeginPosition = Input.GetTouch(0).position;
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {

            //swipe length > 10% screen width is considered to be swipe event
            float offset = Screen.width * 10 / 100;

            touchEndPosition = Input.GetTouch(0).position;
            float diffX = touchEndPosition.x - touchBeginPosition.x;
            if (diffX < -offset)
                doSwipeEvent("L");
            if (diffX > offset)
                doSwipeEvent("R");
        }
    }

    void doSwipeEvent(string direction) {
        if (direction == "R")
            SceneManager.LoadSceneAsync("Progress", LoadSceneMode.Additive);
    }
}

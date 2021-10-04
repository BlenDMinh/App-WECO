using UnityEditor.SceneManagement;
using UnityEditor;

public class MyEditor  {
    [MenuItem("MyScenes/Main Menu &1")]
    public static void MainMenuScene() {
        OpenScene("Main Menu");
    }

    [MenuItem("MyScenes/Community &2")]
    public static void CommunityScene() {
        OpenScene("Community");
    }

    public static void OpenScene(string name) {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
            EditorSceneManager.OpenScene("Assets/Scenes/"+ name +".unity");
        }
    }
}

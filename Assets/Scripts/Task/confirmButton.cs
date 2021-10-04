using UnityEngine;
using UnityEngine.UI;

public class confirmButton : MonoBehaviour {
    public Text status;
    public void toSubmission() {
        status.text = "submit";
        //SceneManager.LoadScene("Submission", LoadSceneMode.Single);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class initInputField : MonoBehaviour {

    public InputField inputField;
    void Start() {
        inputField.text = "#1 What did you do?\n\n#2 How did you do it?\n\n#3 How do you feel?\n\n";
    }
}

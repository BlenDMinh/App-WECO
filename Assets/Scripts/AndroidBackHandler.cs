using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidBackHandler : MonoBehaviour {
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            AppHelper.Instance.LoadScene("");
        }
    }
}

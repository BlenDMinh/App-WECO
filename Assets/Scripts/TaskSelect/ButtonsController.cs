using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour {

    [SerializeField]
    private Animator board, mask;
    public void triggerAnimation() {

        bool state = board.GetBool("isHidden");
        state = !state;

        board.SetBool("isHidden", state);
        mask.SetBool("isHidden", state);
        Debug.Log("Clicked");
    }
}

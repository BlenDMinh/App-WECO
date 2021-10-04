using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour {


    [SerializeField]
    private Animator board, mask;

    //Toggle animation of Switching between OverallTaskBoard and Tasks Select Board
    public void triggerAnimation() {

        bool state = board.GetBool("isHidden");
        state = !state;

        board.SetBool("isHidden", state);
        mask.SetBool("isHidden", state);
    }

    //Use for Confirm Button in Task Config Panel
    public void confirmButton() {
        //to do
        //  - update the selected difficulty in configuring Task Element
        //  - Make an Animation for this button

        TaskSelectDataManager.Instance.currentTE_ID = -1;
    }
}

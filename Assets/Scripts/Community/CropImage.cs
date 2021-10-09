using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropImage : MonoBehaviour {
    public Image image;

    public void FitCanvas() {
        UIHelper.FitImage(image, this.gameObject, "FILL");
    }
}

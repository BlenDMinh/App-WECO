using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropImage : MonoBehaviour {
    public Image image;

    private float canvas_w, canvas_h;

    public void FitCanvas() {
        image.SetNativeSize();
        canvas_w = GetComponent<RectTransform>().sizeDelta.x;
        canvas_h = GetComponent<RectTransform>().sizeDelta.y;
        float img_w = image.GetComponent<RectTransform>().sizeDelta.x;
        float img_h = image.GetComponent<RectTransform>().sizeDelta.y;
        if (img_h < img_w)
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(img_w * (canvas_h / img_h), canvas_h);
        else
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas_w, img_h * (canvas_w / img_w));
    }
}

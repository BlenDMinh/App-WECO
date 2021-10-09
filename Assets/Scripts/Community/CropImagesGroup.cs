using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropImagesGroup : MonoBehaviour {
    public List<CropImage> cropImages;

    public void ViewImage(int id) {
        List<Sprite> images = new List<Sprite>();
        foreach(CropImage i in cropImages)
            images.Add(i.image.sprite);

        ImageViewerControl.OpenImageViewer(images, id);
    }
}

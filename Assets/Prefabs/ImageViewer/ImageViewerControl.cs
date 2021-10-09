using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewerControl : MonoBehaviour {
    [SerializeField]
    private Image displayImage;
    private int id;

    public List<Sprite> images;

    private static GameObject ImageViewer;

    public void DisplayImage(int id) { // Display the id-th image in List<Sprite>
        displayImage.sprite = images[id];
        UIHelper.FitImage(displayImage, displayImage.transform.parent.gameObject, "FIT");
    }

    public void ModifyID(int offset) {
        /*
         * offset = +1 -> next image
         * offset = +2 -> next next image
         * ...
         * offset = -1 -> previous image
         */
        id += offset;
        id = Mathf.Max(0, Mathf.Min(images.Count - 1, id));
        Debug.Log(id);
        DisplayImage(id);
    }

    [SerializeField]
    private GameObject ImageViewer_Prefab;

    public static ImageViewerControl OpenImageViewer() {
        ImageViewer = Instantiate(Resources.Load("Prefabs/ImageViewer/ImageViewer") as GameObject);
        ImageViewerControl ivc = ImageViewer.GetComponent<ImageViewerControl>();
        return ivc;
    }

    public static ImageViewerControl OpenImageViewer(List<Sprite> images) {
        ImageViewerControl ivc = OpenImageViewer();
        ivc.images = images;

        ivc.DisplayImage(0);
        return ivc;
    }

    public static ImageViewerControl OpenImageViewer(List<Sprite> images, int displayID) {
        ImageViewerControl ivc = OpenImageViewer();
        ivc.images = images;
        ivc.DisplayImage(Mathf.Max(0, Mathf.Min(images.Count - 1, displayID)));
        return ivc;
    }

    public static void CloseImageViewer() {
        Destroy(ImageViewer);
    }
}

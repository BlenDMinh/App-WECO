using System.Collections.Generic;
using UnityEngine;

public class TestImageViewer : MonoBehaviour {
    [SerializeField]
    private ImageViewerControl imageViewer;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private int display_id;

    void Start() {
        ImageViewerControl.OpenImageViewer(images, display_id);
    }
}

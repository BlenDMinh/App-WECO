using UnityEngine;
using UnityEngine.UI;

public class UIHelper {
    public static void PushPrefabToParent(GameObject Prefab, Transform parent, float offset) {
        GameObject prefab = GameObject.Instantiate(Prefab, parent);
        RectTransform prefab_r = prefab.GetComponent<RectTransform>();
        RectTransform parent_r = parent.GetComponent<RectTransform>();
        prefab_r.pivot = new Vector2(0.5f, 1); // default pivot
        //parent_r.anchorMin = parent_r.anchorMax = new Vector2(0, 0); //default anchor

        Vector3[] parent_wc = new Vector3[4];
        parent_r.GetWorldCorners(parent_wc);


        float lowest_pos = parent_wc[2].y;

        Vector3[] child_wc = new Vector3[4]; //corner of children in world position

        // The first one in the parent
        if (parent.childCount == 0) {
            prefab_r.SetParent(parent);
            prefab_r.position = new Vector2(0, lowest_pos);
            prefab_r.anchoredPosition = new Vector2(0, prefab_r.anchoredPosition.y);
            return;
        }

        foreach (Transform children in parent) {
            RectTransform child_r = children.GetComponent<RectTransform>();
            child_r.GetWorldCorners(child_wc);
            lowest_pos = Mathf.Min(lowest_pos, child_wc[3].y);
        }

        prefab_r.SetParent(parent);
        prefab_r.position = new Vector2(0, lowest_pos - offset);
        prefab_r.anchoredPosition = new Vector2(0, prefab_r.anchoredPosition.y);
    }
    public static GameObject PushAndGetPrefabToParent(GameObject Prefab, Transform parent, float offset) {
        GameObject prefab = GameObject.Instantiate(Prefab, parent,false);
        RectTransform prefab_r = prefab.GetComponent<RectTransform>();
        RectTransform parent_r = parent.GetComponent<RectTransform>();
        prefab_r.pivot = new Vector2(0.5f, 1); // default pivot
        //parent_r.anchorMin = parent_r.anchorMax = new Vector2(0, 0); //default anchor

        Vector3[] parent_wc = new Vector3[4];
        parent_r.GetWorldCorners(parent_wc);


        float lowest_pos = parent_wc[2].y;

        Vector3[] child_wc = new Vector3[4]; //corner of children in world position

        // The first one in the parent
        if (parent.childCount == 0) {
            prefab_r.SetParent(parent);
            prefab_r.position = new Vector2(0, lowest_pos);
            prefab_r.anchoredPosition = new Vector2(0, prefab_r.anchoredPosition.y);
            return prefab;
        }

        foreach (Transform children in parent) {
            RectTransform child_r = children.GetComponent<RectTransform>();
            child_r.GetWorldCorners(child_wc);
            lowest_pos = Mathf.Min(lowest_pos, child_wc[3].y);
        }

        prefab_r.SetParent(parent);
        prefab_r.position = new Vector2(0, lowest_pos - offset);
        prefab_r.anchoredPosition = new Vector2(0, prefab_r.anchoredPosition.y);
        return prefab;
    }

    public static GameObject CreateImageObject(string path) {
        Sprite sprite = IMG2Sprite.LoadNewSprite(path);
        GameObject image = new GameObject("Image");
        image.AddComponent<Image>().sprite = sprite;
        image.transform.localScale = new Vector3(1, 1, 1);
        return image;
    }

    public static Image LoadImage(string path) {
        GameObject image = CreateImageObject(path);
        Image image_ret = image.GetComponent<Image>();
        Object.Destroy(image);
        return image_ret;
    }

    public static void FitImage(Image image, GameObject canvasObject, string mode) {
        image.SetNativeSize();
        float canvas_w = canvasObject.GetComponent<RectTransform>().sizeDelta.x;
        float canvas_h = canvasObject.GetComponent<RectTransform>().sizeDelta.y;
        float img_w = image.GetComponent<RectTransform>().sizeDelta.x;
        float img_h = image.GetComponent<RectTransform>().sizeDelta.y;

        mode = mode.ToLower();

        if (mode == "fill") {
            if (img_h < img_w)
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(img_w * (canvas_h / img_h), canvas_h);
            else
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas_w, img_h * (canvas_w / img_w));
        } else if (mode == "fit") {
            if (img_h < img_w)
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas_w, img_h * (canvas_w / img_w));
            else
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(img_w * (canvas_h / img_h), canvas_h);
        }
    }
}
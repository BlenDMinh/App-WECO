using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
[RequireComponent(typeof(ContentSizeFitter))]
[RequireComponent(typeof(Text))]
public class DynamicTextBehavior : UIBehaviour {
    public int HorizontalMargin = 0;
    public int VerticalMargin = 0;

    public bool vertical = true;
    public bool horizontal = true;


    public RectTransform ParentRectTransform;
    public RectTransform RectTransform;

    protected override void OnRectTransformDimensionsChange() {
        base.OnRectTransformDimensionsChange();

        Vector3 sizeDelta = RectTransform.sizeDelta;

        if (!vertical)
            sizeDelta.y = ParentRectTransform.sizeDelta.y;
        else
            sizeDelta.y = RectTransform.sizeDelta.y + VerticalMargin * 2;

        if (!horizontal)
            sizeDelta.x = ParentRectTransform.sizeDelta.x;
        else
            sizeDelta.x = RectTransform.sizeDelta.x + HorizontalMargin * 2;

        ParentRectTransform.sizeDelta = sizeDelta;
    }
}
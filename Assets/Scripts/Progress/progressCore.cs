using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class progressCore : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    public RectTransform graphContainer;
    // Start is called before the first frame update
    void Start() {
        Challenge challenge = new Challenge();
        challenge = challenge.ReadCurrentChallenge();
        //List<DailyRecord> record;
        showGraph(new List<int>{9, 4, 5, 1, 3, 1, 2, 9});
    }

    private void showGraph(List<int> val) {
        float H = graphContainer.sizeDelta.y;
        float W = graphContainer.sizeDelta.x;
        float yMax = 9f;
        float xSize = W / (val.Count - 1);
        GameObject lastCircle = null;
        for(int i = 0; i < val.Count; i++) {
            float x = i * xSize;
            float y = (val[i] / yMax) * H;
            GameObject curCircle = createCircle(new Vector2(x, y));
            if (lastCircle != null)
                createDotConnection(curCircle.GetComponent<RectTransform>().anchoredPosition, lastCircle.GetComponent<RectTransform>().anchoredPosition);
            lastCircle = curCircle;
        }
    }
    private GameObject createCircle(Vector2 anchorPos) {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(graphContainer, false);
        circle.GetComponent <Image>().sprite = circleSprite;
        RectTransform r = circle.GetComponent<RectTransform>();
        r.anchoredPosition = anchorPos;
        r.anchorMin = r.anchorMax = new Vector2(0, 0);
        r.sizeDelta = new Vector2(30, 30);
        return circle;
    }

    private void createDotConnection(Vector2 A, Vector2 B) {
        GameObject line = new GameObject("Line", typeof(Image));
        line.transform.SetParent(graphContainer);
        RectTransform r = line.GetComponent<RectTransform>();

        Vector2 dir = (B - A).normalized;
        float dis = Vector2.Distance(A, B);

        r.anchorMin = r.anchorMax = new Vector2(0, 0);
        r.sizeDelta = new Vector2(dis, 10f);
        r.anchoredPosition = A + dir * dis * 0.5f;
        r.localEulerAngles = new Vector3(0, 0, (float) (Math.Atan2(dir.y , dir.x) * (180/Math.PI)));
    }
}

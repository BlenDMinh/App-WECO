using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class progressCore : MonoBehaviour {
    [SerializeField] private Sprite circleSprite;
    public RectTransform canvas;
    public RectTransform graphContainer;
    // Start is called before the first frame update
    float H, W;
    void Start() {
        Challenge challenge = new Challenge();
        challenge = challenge.ReadCurrentChallenge();
         H = graphContainer.sizeDelta.y;
         W = graphContainer.sizeDelta.x;
        showGraph(new List<int>{55, 41, 23, 54, 65, 30, 46, 21, 23, 80, 40});
    }

    private void showGraph(List<int> val) {
        float yMax = 0, yMin = float.MaxValue;
        foreach (int i in val) {
            yMax = Math.Max(yMax, i);
            yMin = Math.Min(yMin, i);
        }

        float offset = yMax - yMin;
        yMax = (float) Math.Ceiling(yMax + offset * (20f / 100));
        yMin = Math.Max(0, (float) Math.Floor(yMin - offset * (20f / 100)));

        yMax = 10f * (float) Math.Ceiling(yMax / 10);
        yMin = 10f * (float) Math.Floor(yMin / 10);

        float xSize = W / (val.Count - 1);

        //Draw line graph

        for(float y = yMin; y <= yMax; y += 10)
            createHorizontalLine((y - yMin) / (yMax - yMin) * H - H * 0.5f, y);


        GameObject lastCircle = null;
        for(int i = 0; i < val.Count; i++) {
            float x = i * xSize;
            float y = ((val[i] - yMin) / (yMax - yMin)) * H;
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
        r.sizeDelta = new Vector2(0, 0);
        return circle;
    }

    private void createDotConnection(Vector2 A, Vector2 B) {
        GameObject line = new GameObject("Line", typeof(Image));
        line.transform.SetParent(graphContainer);
        line.GetComponent<Image>().color = new Color(127f/255, 211f/255, 1f/255);
        RectTransform r = line.GetComponent<RectTransform>();
        Vector2 dir = (B - A).normalized;
        float dis = Vector2.Distance(A, B);

        r.anchorMin = r.anchorMax = new Vector2(0, 0);
        r.sizeDelta = new Vector2(dis * canvas.localScale.x, 7.5f * canvas.localScale.x);
        r.anchoredPosition = A + dir * dis * 0.5f;
        r.localEulerAngles = new Vector3(0, 0, (float) (Math.Atan2(dir.y , dir.x) * (180/Math.PI)));
    }

    private void createHorizontalLine(float y, float val) {
        GameObject line = new GameObject("HLine", typeof(Image));
        line.transform.SetParent(graphContainer);
        line.GetComponent<Image>().color = new Color(217f/255, 217f/255, 217f/255);
        RectTransform r = line.GetComponent<RectTransform>();

        r.sizeDelta = new Vector2(graphContainer.sizeDelta.x * canvas.localScale.x, 3f * canvas.localScale.y);
        r.anchoredPosition = new Vector2(0, y);


        GameObject unit = new GameObject("HUnit", typeof(Text));
        unit.transform.SetParent(graphContainer);
        Text t = unit.GetComponent<Text>();

        t.font = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        t.color = new Color(150f / 255, 150f / 255, 150f / 255);
        t.text = val.ToString();
        t.fontSize = (int) (30 * canvas.localScale.x);
        t.alignment = TextAnchor.MiddleRight;
    
        r = unit.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(50 * canvas.localScale.x, 50 * canvas.localScale.y);
        r.anchoredPosition = new Vector2( -W * 0.55f, y);
    }
}

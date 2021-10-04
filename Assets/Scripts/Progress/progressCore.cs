using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
     
    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
}


public class progressCore : MonoBehaviour {
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Text Name;
    [SerializeField] private Text startDate, endDate;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform graphContainer;
    [SerializeField] private Text number;
    float H, W;

    [SerializeField] private RectTransform chartContainer;
    [SerializeField] private RectTransform[] bars = new RectTransform[3];
    info[][] categories = new info[][] 
        {
            new info[] {new info("Easy", 0), new info("Normal", 0.25), new info("Hard", 0.75)}, 
            new info[] {new info("Short", 0.5), new info("Average", 0), new info("Long", 00.5)}, 
            new info[] {new info("Bad", 0.33), new info("Good", 0.34), new info("Amazing", 0.33)}, 
        };

    // Start is called before the first frame update
    

    [Obsolete]
    void Start() {
        drawGraph();
        buildChart();
    }

    public void drawGraph(){
        H = graphContainer.sizeDelta.y;
        W = graphContainer.sizeDelta.x;
        string challengeName = Challenge.LoadCurrentChallenge().challengeName;
        Name.text = challengeName + " CHALLENGE";
        SortedDictionary<string, List<DailyRecord>> record = UserData.LoadUserData().record;
        if (record.ContainsKey(challengeName)) {
            List<int> list = ConvertToGraph(record[challengeName]);
            Debug.Log("Graph contain:\n");
            // foreach (int i in list)
            //     Debug.Log(i);
            showGraph(list);
        }
    }

    public class info
	{
		public string str;
		public double percent;
		public info(string Str, double Percent)
		{
			str = Str;
			percent = Percent;
		}
	}

    private List<int> ConvertToGraph(List<DailyRecord> record) {
        startDate.text = record[0].date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
        endDate.text = record[record.Count - 1].date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
        List<int> res = new List<int>();
        DateTime date = record[0].date;
        int fishCount = 0;
        foreach(DailyRecord dr in record) {
            while (DateTime.Compare(date, dr.date) < 0) {
                date = date.AddDays(1);
                res.Add(0);
            }
            fishCount += dr.fishCount;
            res.Add(dr.taskCount);
            date = date.AddDays(1);
        }
        number.text = fishCount.ToString();
        return res;
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

        for (float y = yMin; y <= yMax; y += 10) {
            createHorizontalLine((y - yMin) / (yMax - yMin) * H - H * 0.5f);
            addUnit((y - yMin) / (yMax - yMin) * H - H * 0.5f, y);
        }

        for(float x = 0; x < W; x = Math.Min(x + xSize * 5, W))
            createVerticalLine(x - W / 2);
        createVerticalLine(W / 2);

        GameObject lastCircle = null;
        for(int i = 0; i < val.Count; i++) {
            float x = i * xSize;
            float y = ((val[i] - yMin) / (yMax - yMin)) * H;
            GameObject curCircle = createCircle(new Vector2(x, y));
            if (lastCircle != null)
                createDotConnection(curCircle.GetComponent<RectTransform>().anchoredPosition, lastCircle.GetComponent<RectTransform>().anchoredPosition);
            Destroy(lastCircle);
            lastCircle = curCircle;
        }

        for (int i = 0; i < val.Count; i++) {
            GameObject circle = createCircle(new Vector2(i * xSize, ((val[i] - yMin) / (yMax - yMin)) * H));
        }
    }
    private GameObject createCircle(Vector2 anchorPos) {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(graphContainer, false);
        circle.GetComponent <Image>().sprite = circleSprite;
        RectTransform r = circle.GetComponent<RectTransform>();
        r.anchoredPosition = anchorPos;
        r.anchorMin = r.anchorMax = new Vector2(0, 0);
        r.sizeDelta = new Vector2(20 * canvas.localScale.x, 20 * canvas.localScale.x);
        return circle;
    }

    private void createDotConnection(Vector2 A, Vector2 B) {
        GameObject line = new GameObject("Line", typeof(Image));
        line.transform.SetParent(graphContainer);
        line.GetComponent<Image>().color = new Color(65f/255, 237f/255, 48f/255);
        RectTransform r = line.GetComponent<RectTransform>();
        Vector2 dir = (B - A).normalized;
        float dis = Vector2.Distance(A, B);

        r.anchorMin = r.anchorMax = new Vector2(0, 0);
        r.sizeDelta = new Vector2(dis * canvas.localScale.x, 5f * canvas.localScale.x);
        r.anchoredPosition = A + dir * dis * 0.5f;
        r.localEulerAngles = new Vector3(0, 0, (float) (Math.Atan2(dir.y , dir.x) * (180/Math.PI)));
    }

    private void createVerticalLine(float x) {
        GameObject line = new GameObject("VLine", typeof(Image));
        line.transform.SetParent(graphContainer);
        line.GetComponent<Image>().color = new Color(217f / 255, 217f / 255, 217f / 255);
        RectTransform r = line.GetComponent<RectTransform>();

        r.sizeDelta = new Vector2(3f * canvas.localScale.x, graphContainer.sizeDelta.y * canvas.localScale.y);
        r.anchoredPosition = new Vector2(x, 0);
    }

    private void createHorizontalLine(float y) {
        GameObject line = new GameObject("HLine", typeof(Image));
        line.transform.SetParent(graphContainer);
        line.GetComponent<Image>().color = new Color(217f/255, 217f/255, 217f/255);
        RectTransform r = line.GetComponent<RectTransform>();

        r.sizeDelta = new Vector2(graphContainer.sizeDelta.x * canvas.localScale.x, 3f * canvas.localScale.y);
        r.anchoredPosition = new Vector2(0, y);
    }

    private void addUnit(float y, float val) {
        GameObject unit = new GameObject("HUnit", typeof(Text));
        unit.transform.SetParent(graphContainer);
        Text t = unit.GetComponent<Text>();

        t.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        t.color = new Color(0.1f, 0.1f, 0.1f);
        t.text = val.ToString();
        t.fontSize = (int)(30 * canvas.localScale.x);
        t.alignment = TextAnchor.MiddleRight;

        RectTransform r = unit.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(50 * canvas.localScale.x, 50 * canvas.localScale.y);
        r.anchoredPosition = new Vector2(-W * 0.55f, y);
    }
    
    private void buildChart()
    {
        for (int bar = 0; bar < 3; bar++) //xét trên từng thanh
        {
            for (int cat = 0; cat < 3; cat++) //xét trên 3 category
            {
                RectTransform Bar = (RectTransform)bars[bar].GetChild(cat);
                info Category = categories[bar][cat];
                if (Category.percent > 0) 
                {
                    if (cat == 0)
                    {
                        Bar.SetLeft(0);
                        Bar.SetRight((float)(500 - (500*Category.percent)));
                    }
                    if (cat == 1)
                    {
                        float left = (float)(500*categories[bar][cat - 1].percent <= 250? 500*categories[bar][cat - 1].percent : 250);
                        float right = (float)(500*categories[bar][cat + 1].percent <= 250? 500*categories[bar][cat + 1].percent : 250);
                        Bar.SetLeft(left);
                        Bar.SetRight(right);
                    }
                    if (cat == 2)
                    {
                        Bar.SetLeft((float)(500 - (500*Category.percent)));
                        Bar.SetRight(0);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskCore : MonoBehaviour {

    public GameObject reqContainer;
    public GameObject reqBox;
    public Text taskDesc;
    public Button confirmButton;

    private Task task;
    private List<Slider> reqList;

    void Start() {
        float curY, off;
        off = reqContainer.GetComponent<RectTransform>().anchoredPosition.y;
        curY = reqContainer.GetComponent<RectTransform>().sizeDelta.y;
        task = Task.LoadCurrentTask();
        taskDesc.text = "\n" + task.taskDescription;

        foreach(KeyValuePair<string, int> req in task.requirement) {
            confirmButton.interactable = false;
            GameObject obj = Instantiate(reqBox, reqContainer.transform);
            RectTransform r = obj.GetComponent<RectTransform>();
            r.anchoredPosition = new Vector2(0, curY + off);
            GameObject minVal = obj.transform.Find("minVal").gameObject;
            GameObject maxVal = obj.transform.Find("maxVal").gameObject;
            GameObject unit = obj.transform.Find("unit").gameObject;
            Slider slider = obj.transform.Find("Slider").GetComponent<Slider>();

            minVal.GetComponent<Text>().text = "0";
            maxVal.GetComponent<Text>().text = req.Value.ToString();
            unit.GetComponent<Text>().text = req.Key;
            slider.maxValue = req.Value;

            reqList.Add(slider);

            curY -= r.sizeDelta.y;
            reqContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(reqContainer.GetComponent<RectTransform>().sizeDelta.x, -curY);
        }
    }

    private void Update() {
        bool isAccept = true;
        foreach (Slider slider in reqList)
            if (slider.value < slider.maxValue) {
                isAccept = false;
                break;
            }
        if (isAccept)
            confirmButton.interactable = true;
    }
}

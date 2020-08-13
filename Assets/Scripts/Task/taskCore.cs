using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class taskCore : MonoBehaviour {

    public GameObject reqContainer;
    public GameObject reqBox;
    public Text taskDesc;
    public Button confirmButton;
    public Text status;

    private Challenge challenge;
    private UserData user;
    private Task task;
    List<Slider> reqList;

    [Obsolete]
    void Start() {
        challenge = Challenge.LoadCurrentChallenge();
        user = UserData.LoadUserData();
        reqList = new List<Slider>();
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

            Dictionary<string, int> progress = user.taskProgress[challenge.challengeName][task.id];
            slider.value = progress[req.Key];

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
        else
            confirmButton.interactable = false;
        if (status == null)
            return;
        string s = status.text;

        //Save current task progress
        if (s == "exit" || s == "submit") {
            Dictionary<string, int> progress = new Dictionary<string, int>();
            for(int i = 0; i < task.requirement.Count; i++)
                progress.Add(reqList[i].transform.parent.Find("unit").GetComponent<Text>().text,(int) reqList[i].value);
            if (user.taskProgress == null)
                user.taskProgress = new SortedDictionary<string, List<Dictionary<string, int>>>();
            if (!user.taskProgress.ContainsKey(challenge.challengeName)) {
                user.taskProgress.Add(challenge.challengeName, new List<Dictionary<string, int>>());
                for (int j = 0; j < challenge.tasks.Count; j++)
                    user.taskProgress[challenge.challengeName].Add(new Dictionary<string, int>());
            }
            if (user.taskProgress[challenge.challengeName].Count == 0)
                for (int j = 0; j < challenge.tasks.Count; j++)
                    user.taskProgress[challenge.challengeName].Add(new Dictionary<string, int>());
            user.taskProgress[challenge.challengeName][task.id] = progress;
            UserData.SaveUserData(user);
        }


        if (s == "exit")
            SceneManager.UnloadSceneAsync("Task");
        if (s == "submit")
            SceneManager.LoadScene("Submission");
    }
}

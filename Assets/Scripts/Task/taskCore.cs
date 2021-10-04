using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class taskCore : MonoBehaviour {

    /* reContainer là box chứa dữ liệu user đã hoàn thành task được tới đâu
     * (tức là chứa những thanh slider của task, tùy từng task mà sẽ có 1,2,3... thanh slider)
     */
    public GameObject reqContainer;

    // reqBox là prefab thanh kéo slider progress của người dùng
    public GameObject reqBox;

    // Chứa những thông tin cụ thể về task
    public Text taskDesc;

    // Nút hoàn thành task
    public Button confirmButton;

    public Text status;

    private Challenge challenge;
    private UserData user;
    private Task task;
    List<Slider> reqList;

    [Obsolete]
    void Start() {

        // Load challenge hiện tại lên
        challenge = Challenge.LoadCurrentChallenge();

        // Load data của user hiện tại lên
        user = UserData.LoadUserData();
        reqList = new List<Slider>();

        // Vị trí y của 
        float curY;
        curY = reqContainer.GetComponent<RectTransform>().sizeDelta.y;

        // Load task hiện tại lên
        task = Task.LoadCurrentTask();

        // Hiện task description
        taskDesc.text = "\n" + task.taskDescription;

        foreach(KeyValuePair<string, int> req in task.requirement) {
            confirmButton.interactable = false;

            // Tạo từng thanh slider cho từng requirements của task
            GameObject obj = Instantiate(reqBox, reqContainer.transform);

            // r là RectTransform của obj, đổi giá trị của r thì position của obj cũng thay đổi theo
            RectTransform r = obj.GetComponent<RectTransform>();
            r.anchorMax = r.anchorMin = new Vector2(0.5f, 1);
            r.anchoredPosition = new Vector2(0, curY);

            // Lấy từng giá trị minVal, maxVal, unit từ thanh slider hiện tại
            GameObject minVal = obj.transform.Find("minVal").gameObject;
            GameObject maxVal = obj.transform.Find("maxVal").gameObject;
            GameObject unit = obj.transform.Find("unit").gameObject;
            Slider slider = obj.transform.Find("Slider").GetComponent<Slider>();
            InputField inputField = obj.transform.Find("InputField").GetComponent<InputField>();

            // Gán thông 3 thông số minVal, maxVal, unit cho thanh slider hiện tại
            minVal.GetComponent<Text>().text = "0";
            maxVal.GetComponent<Text>().text = req.Value.ToString();
            unit.GetComponent<Text>().text = req.Key;
            slider.maxValue = req.Value;

            //  Lấy progress hiện tại của task
            Dictionary<string, int> progress = UserData.GetTaskProgress(user, challenge)[task.id];

            // Gán giá trị của slider trong k
            if (progress.ContainsKey(req.Key))
                slider.value = progress[req.Key];
            else
                slider.value = 0;

            // Ô nhập số trong task phụ thuộc vào slider
            inputField.text = slider.value.ToString();

            reqList.Add(slider);

            curY -= r.sizeDelta.y;
            reqContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(reqContainer.GetComponent<RectTransform>().sizeDelta.x, -curY);
        }
    }

    private void Update() {
        bool isAccept = true;

        // Update value của các slider trong task
        foreach (Slider slider in reqList)
        {
            if (slider.value < slider.maxValue)
            {
                isAccept = false;
                break;
            }
        }
        UpdateEmoji();

        if (isAccept)
            confirmButton.interactable = true;
        else
            confirmButton.interactable = false;
        if (status == null)
            return;
        string s = status.text;

        //Save current task progress
        // Cái này đúng ra phải viết thành 1 hàm ặc ặc ặc =)))
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

    public Image emojiTexture;
    public TMPro.TMP_Text encourageText;

    // Hàm thay đổi emoji dựa trên phần trăm hoàn thành của task
    private void UpdateEmoji()
    {
        // Lấy % task hiện tại đã hoàn thành
        double percentage = task.TaskProgressOverall(reqList);
        string emojiName = "";

        if (percentage == 0)
        {
            emojiName = "Sad";
        }
        else if (percentage > 0 && percentage <= (double)1 / 3)
        {
            emojiName = "PrettySad";
        }
        else if (percentage > (double)1 / 3 && percentage <= (double)2 / 3)
        {
            emojiName = "Normal";
        }
        else if (percentage > (double)2 / 3 && percentage < 1)
        {
            emojiName = "PrettyHappy";
        }
        else if (percentage == 1)
        {
            emojiName = "Happy";
        }

        /*
        if (percentage >= 0 && percentage < 0.2)
        {
            emojiName = "Sad";
        }
        else if (percentage >= 0.2 && percentage < 0.4)
        {
            emojiName = "PrettySad";
        }
        else if (percentage >= 0.4 && percentage < 0.6)
        {
            emojiName = "Normal";
        }
        else if (percentage >= 0.6 && percentage < 0.8)
        {
            emojiName = "PrettyHappy";
        }
        else if (percentage >= 0.8 && percentage <= 1)
        {
            emojiName = "Happy";
        }
        */

        Sprite emoji = Resources.Load<Sprite>("emoji\\" + emojiName);
        emojiTexture.sprite = emoji;
    }
}

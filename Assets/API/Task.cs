using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Task {
	public string taskName, taskDescription;
	public Reward reward;
    public float x, y;
	public Dictionary<string, int> requirement;
	public int id;

	public static Task LoadCurrentTask() {
		StreamReader r;
		if (Application.platform == RuntimePlatform.Android)
			r = new StreamReader(Application.persistentDataPath + "//Data//task.json");
		else
			r = new StreamReader(Application.dataPath + "//Data//task.json");
		string json = r.ReadToEnd();
		Task res = JsonConvert.DeserializeObject<Task>(json);
		r.Close();
		return res;

        /* task.json chỉ có duy nhất 1 task, và đó là task hiện tại của user, đã được
         * lưu lúc load journey
         */
	}

    public double TaskProgressOverall(List<Slider> listSlider)
    {
        /* listSlider là để lấy progress hiện tại (ngay tại thời điểm người 
         * dùng kéo thanh slider
         */ 
        // Lấy challenge hiện tại
        Challenge currentChallenge = Challenge.LoadCurrentChallenge();

        // Lấy data user hiện tại
        UserData user = UserData.LoadUserData();

        // Lấy information của task hiện tại
        Task task = LoadCurrentTask();



        // Tạo ra list taskProgress và taskRequired để dễ dàng tính % hơn
        List<int> taskProgress = new List<int>();
        foreach (var item in listSlider)
        {
            taskProgress.Add(Convert.ToInt32(item.value));
        }
        List<int> taskRequired = new List<int>();
        foreach (var item in task.requirement)
        {
            taskRequired.Add(item.Value);
        }

        double percentage = 0; // Biến tính phần trăm hoàn thành của task
        // Tính toán
        for (int i = 0; i < taskRequired.Count; i++)
        {
            percentage += (double)taskProgress[i] / (double)taskRequired[i];
        }
        percentage = percentage / 2;
        return percentage;
    }
}

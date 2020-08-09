using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Task {
	public string taskName, taskDescription;
	public Reward reward;
    public float x, y;
	public Dictionary<string, int> requirement;

	public static Task LoadCurrentTask() {
		StreamReader r;
		if (Application.platform == RuntimePlatform.Android)
			r = new StreamReader(Application.persistentDataPath + "//Data//task.json");
		else
			r = new StreamReader(Application.dataPath + "//Data//task.json");
		string json = r.ReadToEnd();
		Task res = JsonConvert.DeserializeObject<Task>(json);
		return res;
	}
}

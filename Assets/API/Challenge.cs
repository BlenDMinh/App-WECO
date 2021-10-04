using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Challenge {
    public string challengeName, challengeDescription;
	public float bgW = 1000, bgH = 1000;
	public float btW = 10, btH = 10;
	public List<Task> tasks;
	public List<Task> todos;
	List<bool> completed;

	public static Challenge LoadCurrentChallenge() {
		StreamReader r;
		if (Application.platform == RuntimePlatform.Android)
			r = new StreamReader(Application.persistentDataPath + "//Data//challenge.json");
		else
			r = new StreamReader(Application.dataPath + "//Data//challenge.json");
		string json = r.ReadToEnd();
		Challenge res = JsonConvert.DeserializeObject<Challenge>(json);
		r.Close();
		return res;
	}

    public static List<Challenge> LoadSaveChallenge()
    {
        StreamReader reader;
        if (Application.platform == RuntimePlatform.Android)
            reader = new StreamReader(Application.persistentDataPath + "//Data//challengeSaveOnline.json");
        else
            reader = new StreamReader(Application.dataPath + "//Data//challengeSaveOnline.json");

        string json = reader.ReadToEnd();
        var saveChallengeOnline = JsonConvert.DeserializeObject<List<Challenge>>(json);
        return saveChallengeOnline;
    }
}
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class Challenge {
    public string challengeName, challengeDescription;
    public Reward challengeReward;
	int requiredScore;

	public List<Task> tasks;
	List<bool> completed;
	
	void getReward() {
		for (int i = 0; i < tasks.Count; i++)
			if (completed[i])
				challengeReward.coin += tasks[i].reward.coin;
	}

	public static Challenge LoadCurrentChallenge() {
		StreamReader r = new StreamReader(Application.persistentDataPath + "//Data//challenge.json");
		string json = r.ReadToEnd();
		Challenge res = JsonConvert.DeserializeObject<Challenge>(json);
		return res;
	}
}
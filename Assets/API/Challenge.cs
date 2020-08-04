using System.IO;
using System.Collections.Generic;
using UnityEngine;

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

	public Challenge ReadCurrentChallenge() {
		StreamReader r = new StreamReader(Application.dataPath + "\\challenge.json");
		string json = r.ReadToEnd();
		Challenge res = JsonUtility.FromJson<Challenge>(json);
		return res;
	}
}
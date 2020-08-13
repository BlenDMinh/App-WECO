using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class submitButton : MonoBehaviour {

    bool inRange(float num, float low, float high) {
        return (num < high && num > low);
    }

    public void submit() {
        Task task = Task.LoadCurrentTask();
        UserData user = UserData.LoadUserData();
        Challenge challenge = Challenge.LoadCurrentChallenge();

        if (!user.record.ContainsKey(challenge.challengeName))
            user.record.Add(challenge.challengeName, new List<DailyRecord>());
        List<DailyRecord> record = user.record[challenge.challengeName];
        if (record.Count > 0 && DateTime.Compare(DateTime.Now.Date, record[record.Count - 1].date) == 0) {
            record[record.Count - 1].taskCount++;
            record[record.Count - 1].fishCount++;
            record[record.Count - 1].date = DateTime.Now.Date;
        } else
            record.Add(new DailyRecord(DateTime.Now.Date, 1, 1));
        user.record[challenge.challengeName] = record;
        List<bool> progress = UserData.GetChallengeProgress(user, challenge);
        progress[task.id] = true;
        user.challengeProgress[challenge.challengeName] = progress;
        UserData.SaveUserData(user);
        SceneManager.LoadScene("Journey");
    }
}

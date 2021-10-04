using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class submitButton : MonoBehaviour
{
    public void submit() {

        // Lấy data task hiện tại đang 
        Task task = Task.LoadCurrentTask();

        // Lấy data user hiện tại
        UserData user = UserData.LoadUserData();

        // Lấy data challenge hiện tại
        Challenge challenge = Challenge.LoadCurrentChallenge();

        // Trong trường hợp user chưa có data nào trong ngày
        if (user.record == null)
            user.record = new SortedDictionary<string, List<DailyRecord>>();

        // Trong trường hợp user chưa có record nào về challenge hiện tại thì thêm vào
        if (!user.record.ContainsKey(challenge.challengeName))
            user.record.Add(challenge.challengeName, new List<DailyRecord>());

        // Xử lý data của người sử dụng trong ngày thông quá biến tạm record
        List<DailyRecord> record = user.record[challenge.challengeName];

        /* Nếu time tạo record = time hiện tại
         * (time bắt đầu làm task, thực ra mặc định là lúc mở submission lên)
         * điều kiện là record.Count > 0 nhưng mà nó lúc nào cũng > 0 :))
         */
        if (record.Count > 0 && DateTime.Compare(DateTime.Now.Date, record[record.Count - 1].date) == 0) {
            record[record.Count - 1].taskCount++;
            record[record.Count - 1].fishCount++;
            record[record.Count - 1].date = DateTime.Now.Date;
        } else
            record.Add(new DailyRecord(DateTime.Now.Date, 1, 1));

        // Lưu lại record vào data người dùng
        user.record[challenge.challengeName] = record;

        List<bool> progress = UserData.GetChallengeProgress(user, challenge);

        // task.id chính là stt của task hiện tại
        progress[task.id] = true;

        // Cập nhật tiến độ challenge hiện tại
        user.challengeProgress[challenge.challengeName] = progress;

        // Lưu lại submission của người dùng
        SaveSubmission(user, task.id, challenge.challengeName);

        // Then save user data
        UserData.SaveUserData(user);
        SceneManager.LoadScene("Journey");
    }

    public GameObject submitDifficulty;
    public GameObject submitTime;
    public GameObject submitFeeling;

    private void SaveSubmission(UserData user, int idSubmission, string challengeName)
    {
        Submission submitForm = new Submission()
        {
            difficulty = Convert.ToInt32(submitDifficulty.GetComponent<Slider>().value),
            time = Convert.ToInt32(submitTime.GetComponent<Slider>().value),
            feeling = Convert.ToInt32(submitFeeling.GetComponent<Slider>().value),
        };

        if (user.submissionList == null)
            user.submissionList = new SortedDictionary<string, List<Submission>>();

        if (user.submissionList.ContainsKey(challengeName) == false)
        {
            user.submissionList.Add(challengeName, new List<Submission>());
        }

        user.submissionList[challengeName].Add(submitForm);
        // user.submissionList[challengeName][idSubmission] = submitForm;
    }
}

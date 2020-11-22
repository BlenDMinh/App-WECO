using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveChallenge1 : MonoBehaviour
{
    public GameObject container;
    public GameObject template;

    private UserData user;
    public void LoadChallengeInfo1()
    {
        user = UserData.LoadUserData();
        List<Challenge> challengeList = Challenge.LoadSaveChallenge();

        int i = 0;
        float templateHigh = 40f;

        template.gameObject.SetActive(true);

        foreach (var challenge in challengeList)
        {
            GameObject challengeRow = Instantiate(template, container.transform);
            RectTransform challengeRowRectTransform = challengeRow.GetComponent<RectTransform>();
            challengeRowRectTransform.anchoredPosition = new Vector2(0, 150 - templateHigh * i);

            challengeRow.transform.GetChild(0).GetComponent<Text>().text = challenge.challengeName;
            challengeRow.transform.GetChild(1).GetComponent<Text>().text = challenge.challengeDescription;

            if (i % 2 == 0)
            {
                challengeRow.GetComponent<Image>().color = new Color32(197, 236, 128, 255);
            }
            else
            {
                challengeRow.GetComponent<Image>().color = new Color32(233, 211, 135, 255);
            }

            challengeRow.gameObject.SetActive(true);
            i++;
        }
    }
}

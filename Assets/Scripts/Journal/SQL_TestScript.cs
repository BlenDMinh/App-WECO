using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using TMPro;

public class SQL_TestScript : MonoBehaviour
{
    private string connectionString = @"
        Data Source = 127.0.0.1;
        Initial Catalog = WECO_WECOPIA_DB;
        User ID = sa;
        Password = D23danhserver;
        ";
    static private int numOfUsers = 0;
    // Need to create comparer
    private static SortedDictionary<int, UserData> listUserDataSQL = new SortedDictionary<int, UserData>();
    private void Awake()
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        Debug.Log("Connection Open!!");

        string commandText = "SELECT * FROM dbo.LEADERBOARD";
        SqlCommand command = new SqlCommand(commandText, connection);

        SqlDataReader dataReader = command.ExecuteReader();
        // Each time read one row
        while (dataReader.Read())
        {
            UserData userData = new UserData();
            userData.userName = dataReader.GetString(0);
            userData.totalFish = dataReader.GetInt32(1);
            userData.isNHH = dataReader.GetBoolean(2);
            listUserDataSQL.Add(numOfUsers, userData);
            Debug.Log("Added userData " + numOfUsers);
            numOfUsers++;
        }
        Debug.Log("Connect SUCESS");
    }

    public GameObject container;
    public GameObject template;

    public void DrawDashboard()
    {
        template.gameObject.SetActive(true);

        float templateHigh = 133f;

        int i = 0;
        foreach (var userData in listUserDataSQL)
        {
            GameObject userScore = Instantiate(template, container.transform);
            RectTransform userScoreRectTransform = userScore.GetComponent<RectTransform>();
            userScoreRectTransform.anchoredPosition = new Vector2(0, 364 - templateHigh * i);

            userScore.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            userScore.transform.GetChild(1).GetComponent<Text>().text = userData.Value.userName;
            userScore.transform.GetChild(2).GetComponent<Text>().text = userData.Value.totalFish.ToString();

            if (i % 2 == 0)
            {
                userScore.GetComponent<Image>().color = new Color32(137, 212, 212, 255);
            }
            else
            {
                userScore.GetComponent<Image>().color = new Color32(243, 178, 157, 255);
            }

            userScore.gameObject.SetActive(true);
            i++;
        }
    }
}

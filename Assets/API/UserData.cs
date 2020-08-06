using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserData {
    public bool isNHH;
    public string userName;
    public bool isEmptyUserJournal;
    public int currentChallenge;
    public SortedDictionary<string, List<DailyRecord>> record;
    public string usersJournal;

    private FileStream userDataJsonFile;

    [System.Obsolete]
    public static UserData LoadUserData() {
        // Load from json file
        if (Application.platform == RuntimePlatform.Android) {
            string path = Application.persistentDataPath + "//Data//UserData.json";
            FileStream userDataJsonFile = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(userDataJsonFile);
            string json = reader.ReadToEnd();
            UserData loadDataJson = JsonConvert.DeserializeObject<UserData>(json);
            reader.Close();
            return loadDataJson;
        } else {
            string path = Application.dataPath + "//Data//UserData.json";
            FileStream userDataJsonFile = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(userDataJsonFile);
            string json = reader.ReadToEnd();
            UserData loadDataJson = JsonConvert.DeserializeObject<UserData>(json);
            reader.Close();
            return loadDataJson;
        }
    }
    public void SaveUserData() {
        // Load from json file
    }
    public void SaveUserJournal(string newUsersJournal) {
        UserData loadDataJson = LoadUserData(); // for saving json file
        loadDataJson.usersJournal = newUsersJournal;

        this.usersJournal = newUsersJournal; // for data object

        string json = JsonUtility.ToJson(loadDataJson);
        string path = Application.persistentDataPath + "//Data//UserData.json";
        userDataJsonFile = new FileStream(path, FileMode.Create);
        StreamWriter writer = new StreamWriter(userDataJsonFile);
        json = FormatJson(json);
        writer.WriteLine(json);

        writer.Close();
    }
    public static string FormatJson(string str) {
        str = (str ?? "").Replace("{}", @"\{\}").Replace("[]", @"\[\]");

        int INDENT_SIZE = 4;
        var inserts = new List<int[]>();
        bool quoted = false, escape = false;
        int depth = 0/*-1*/;

        for (int i = 0, N = str.Length; i < N; i++) {
            var chr = str[i];

            if (!escape && !quoted)
                switch (chr) {
                    case '{':
                    case '[':
                    inserts.Add(new[] { i, +1, 0, INDENT_SIZE * ++depth });
                    //int n = (i == 0 || "{[,".Contains(str[i - 1])) ? 0 : -1;
                    //inserts.Add(new[] { i, n, INDENT_SIZE * ++depth * -n, INDENT_SIZE - 1 });
                    break;
                    case ',':
                    inserts.Add(new[] { i, +1, 0, INDENT_SIZE * depth });
                    //inserts.Add(new[] { i, -1, INDENT_SIZE * depth, INDENT_SIZE - 1 });
                    break;
                    case '}':
                    case ']':
                    inserts.Add(new[] { i, -1, INDENT_SIZE * --depth, 0 });
                    //inserts.Add(new[] { i, -1, INDENT_SIZE * depth--, 0 });
                    break;
                    case ':':
                    inserts.Add(new[] { i, 0, 1, 1 });
                    break;
                }

            quoted = (chr == '"') ? !quoted : quoted;
            escape = (chr == '\\') ? !escape : false;
        }

        if (inserts.Count > 0) {
            var sb = new System.Text.StringBuilder(str.Length * 2);

            int lastIndex = 0;
            foreach (var insert in inserts) {
                int index = insert[0], before = insert[2], after = insert[3];
                bool nlBefore = (insert[1] == -1), nlAfter = (insert[1] == +1);

                sb.Append(str.Substring(lastIndex, index - lastIndex));

                if (nlBefore) sb.AppendLine();
                if (before > 0) sb.Append(new string(' ', before));

                sb.Append(str[index]);

                if (nlAfter) sb.AppendLine();
                if (after > 0) sb.Append(new string(' ', after));

                lastIndex = index + 1;
            }

            str = sb.ToString();
        }

        return str.Replace(@"\{\}", "{}").Replace(@"\[\]", "[]");
    }
}

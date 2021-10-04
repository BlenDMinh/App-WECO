using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppHelper : MonoBehaviour
{
    public static AppHelper Instance {
        get {
            if (mInstance == null) {
                lock (mSingletonLock) {
                    mInstance = (AppHelper) FindObjectOfType(typeof(AppHelper));
                    if (mInstance == null) {
                        GameObject obj = new GameObject(typeof(AppHelper).Name);
                        mInstance = obj.AddComponent<AppHelper>();
                    }
                    DontDestroyOnLoad(mInstance);
                }
            }
            return mInstance;
        }
    }
    private static object mSingletonLock = new object();
    private static AppHelper mInstance;

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}

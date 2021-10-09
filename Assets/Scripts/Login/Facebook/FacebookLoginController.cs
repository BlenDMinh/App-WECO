using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using UnityEngine.Networking;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class FacebookLoginController : MonoBehaviour {

    private AccessToken token;

    private void InitCallback() {
        if (FB.IsInitialized) {
            Debug.Log("Initialize Facebook SDK successfully");
            FB.ActivateApp();
            Debug.Log("Checking Login Status");
            FB.Android.RetrieveLoginStatus(LoginStatusCallback);
        }
        else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown) {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void Start() {
        if(!FB.IsInitialized) {
            FB.Init(InitCallback, OnHideUnity);
        }
    }

    // Previous Login Check
    private void LoginStatusCallback(ILoginStatusResult result) {
        if (!string.IsNullOrEmpty(result.Error)) {
            Debug.Log("Error: " + result.Error);
        }
        else if (result.Failed) {
            Debug.Log("Failure: Access Token could not be retrieved");
        }
        else {
            token = result.AccessToken;
            Debug.Log("Success: " + result.AccessToken.UserId);
            GetCommunityProfile(token);
        }
    }

    // Button - Login to Facebook
    public void LoginToFacebook() {
        var perms = new List<string>() {"public_profile", "email", "gaming_user_picture" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    // -> Check Login
    private void AuthCallback(ILoginResult result) {
        if (FB.IsLoggedIn) {
            token = AccessToken.CurrentAccessToken;
            Debug.Log(token.UserId);
            foreach (string perm in token.Permissions) {
                Debug.Log(perm);
            }
            GetCommunityProfile(token);
        }
        else {
            Debug.Log("User cancelled login");
        }
    }

    private string fb_name, fb_avatar_link, fb_avatar_name;

    private void FBGetProfile(IGraphResult result) {
        string json = result.RawResult;
        var dict = Json.Deserialize(json) as Dictionary<string, object>;
        fb_name = dict["name"].ToString();
        // I wish this was Python :(
        fb_avatar_link = ((Dictionary<string, object>)((Dictionary<string, object>) dict["picture"])["data"])["url"].ToString();
        StartCoroutine(CreateCommunityProfile());
    }

    private async void GetCommunityProfile(AccessToken token) {
        string json = await FirebaseHelper.DownloadString("user/FB" + token.UserId);
        if(string.IsNullOrEmpty(json)) { // => new user
            Debug.Log("Creating new profile");
            FB.API("me?fields=name,picture", HttpMethod.GET, FBGetProfile);
            return;
        }

        CommunityUserData.WriteToCache(json);
        SceneManager.LoadScene("Community");
    }

    IEnumerator CreateCommunityProfile() {
        yield return StartCoroutine(DownloadImage(fb_avatar_link));
        CommunityUserData user = new CommunityUserData();
        user.name = fb_name;
        user.avatar = fb_avatar_name;
        user.uid = "FB" + AccessToken.CurrentAccessToken.UserId;
        user.WriteToCache();
        SceneManager.LoadScene("Community");
    }

    IEnumerator DownloadImage(string url) {
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            }
            else {
                string datePatt = @"yyyyMMddHHmmssfff";
                string file_name = DateTime.UtcNow.ToString(datePatt, CultureInfo.InvariantCulture) + ".jpg";

                string savePath = $"{FirebaseHelper.LocalCacheImagePath}{file_name}";
                System.IO.File.WriteAllText(savePath, www.downloadHandler.text);
                fb_avatar_name = file_name;
            }
        }
    } 
}

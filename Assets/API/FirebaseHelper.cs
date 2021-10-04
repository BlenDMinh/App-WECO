using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseHelper : MonoBehaviour {

    public static string LocalCacheImagePath = Application.persistentDataPath + "cache/images/";

    static FirebaseStorage storage = FirebaseStorage.GetInstance(@"gs://wecopia-c8f12.appspot.com");
    public static void UploadFile(string localURL, string saveName, string saveAt) {
        StorageReference fileRef = storage.RootReference.Child(saveAt + "/" + saveName);
        fileRef.PutFileAsync(localURL).ContinueWith((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                StorageMetadata metadata = task.Result;
                string md5Hash = metadata.Md5Hash;
                Debug.Log("Finished uploading...");
                Debug.Log("md5 hash = " + md5Hash);
            }
        });
    }

    public static void UploadBytes(byte[] bytes, string saveName, string saveAt) {
        StorageReference postTextRef = storage.RootReference.Child(saveAt + "/" + saveName);

        postTextRef.PutBytesAsync(bytes).ContinueWith((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                StorageMetadata metadata = task.Result;
                string md5Hash = metadata.Md5Hash;
                Debug.Log("Finished uploading...");
                Debug.Log("md5 hash = " + md5Hash);
            }
        });
    }
    
    public async static System.Threading.Tasks.Task DownloadFile(string URL, string localURL) {
        if (string.IsNullOrEmpty(URL)) {
            throw new ArgumentException($"'{nameof(URL)}' cannot be null or empty.", nameof(URL));
        }

        StorageReference reference = storage.RootReference.Child(URL);

        // Download to the local filesystem
        await reference.GetFileAsync(localURL).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled) {
                Debug.Log($"Finished downloading {URL}!");
            }
        });
    }

    public static async Task<byte[]> DownloadBytes(string URL) {

        if (string.IsNullOrEmpty(URL)) {
            throw new ArgumentException($"'{nameof(URL)}' cannot be null or empty.", nameof(URL));
        }

        StorageReference reference = storage.RootReference.Child(URL);

        byte[] result = null;
        // Download to the local filesystem
        await reference.GetBytesAsync(1 * 1024 * 1024).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
            }
            else {
                byte[] output = task.Result;
                result = output;
                Debug.Log($"Finished downloading {URL}!");
            }
        });

        return result;
    }

    public static async Task<string> DownloadString(string URL) {

        if (string.IsNullOrEmpty(URL)) {
            throw new ArgumentException($"'{nameof(URL)}' cannot be null or empty.", nameof(URL));
        }

        StorageReference reference = storage.RootReference.Child(URL);

        byte[] bytes = null;
        // Download to the local filesystem
        bytes = await DownloadBytes(URL);
        string result = Encoding.ASCII.GetString(bytes);

        return result;
    }

    public static async System.Threading.Tasks.Task DownloadCacheImages(List<string> images) {
        foreach (string image in images) {
            string path = LocalCacheImagePath;
            Directory.CreateDirectory(path);

            if (!File.Exists($"{path}{image}")) {
                Debug.Log($"Can't find image in {path}{image}, start downloading from images/{image}");
                await FirebaseHelper.DownloadFile($"images/{image}", $"{path}{image}");
            }
        }
    }

}

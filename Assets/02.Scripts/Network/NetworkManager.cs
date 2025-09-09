using NUnit.Framework.Constraints;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class NetworkManager : Singleton<NetworkManager> {
    // �α���
    public IEnumerator Signin(SigninData signinData, Action success, Action<int> failure) {
        string jsonString = JsonUtility.ToJson(signinData);
        byte[] byteRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "users/login", UnityWebRequest.kHttpVerbPOST)) {
            www.uploadHandler = new UploadHandlerRaw(byteRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) {
                // TODO : ���� ���� ������ ���� �˸�
            }
            else {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SigninResult>(resultString);

                if (result.result == 2) {
                    var cookie = www.GetResponseHeader("set-cookie");
                    if (!string.IsNullOrEmpty(cookie)) {
                        int lastIndex = cookie.LastIndexOf(';');
                        string sid = cookie.Substring(0, lastIndex);

                        // ����
                        PlayerPrefs.SetString("sid", sid);
                    }

                    success?.Invoke();
                }
                else {
                    failure?.Invoke((int)result.result);
                }
            }
        }

        yield return null;
    }

    // ȸ�� ����
    public IEnumerator RegisterUser(RegisterData signinData, Action success, Action<int> failure) {
        string jsonString = JsonUtility.ToJson(signinData);
        byte[] byteRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "users/signup", UnityWebRequest.kHttpVerbPOST)) {
            www.uploadHandler = new UploadHandlerRaw(byteRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) {
                // TODO : ���� ���� ������ ���� �˸�
            }
            else {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SigninResult>(resultString);
                
                if (result.result == 2) {
                    var cookie = www.GetResponseHeader("set-cookie");
                    success?.Invoke();
                }
                else {
                    failure?.Invoke((int)result.result);
                }
            }
        }

        yield return null;
    }
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
    }
}

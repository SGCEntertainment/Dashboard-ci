using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

public class RequestManager : MonoBehaviour
{
    const string get_config_default_environment = "https://remote-config-api.uca.cloud.unity3d.com/configs?projectId=";

    [SerializeField] Root root;

    private void Start()
    {
        //StartCoroutine(GetAccessTocken(new AuthData("Unitydatastorage@gmail.com", "qwerty12345Q", "PASSWORD"), (login_data) => { Debug.Log(login_data.access_token); }));
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(get_config_default_environment + "925390ba-f412-412d-af1e-dd5867ace799");
        webRequest.SetRequestHeader("Authorization", "Bearer " + "ektV22aUMBvM_NoG7tJDPCDaf-UOmbdCHHo1sPuurHI009f");
        yield return webRequest.SendWebRequest();

        Debug.Log(webRequest.downloadHandler.text);
        root = JsonUtility.FromJson<Root>(webRequest.downloadHandler.text);
    }

    IEnumerator GetAccessTocken(AuthData authData, Action<LoginData> loginData)
    {
        UnityWebRequest webRequest = new UnityWebRequest("https://api.unity.com/v1/core/api/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(authData));

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            loginData.Invoke(JsonUtility.FromJson<LoginData>(webRequest.downloadHandler.text));
        }
    }
}

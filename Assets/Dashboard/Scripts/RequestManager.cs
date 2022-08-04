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
        //StartCoroutine(GetRequest());
        StartCoroutine(UpdateConfig());
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(get_config_default_environment + "af54b501-0dc8-4a9e-8706-729936028bee");
        webRequest.SetRequestHeader("Authorization", "Bearer " + "Aa23Yy_JKmoJzXUa9G8yxouJQHPBigKeM2faJ6eyJrA009f");
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

    IEnumerator UpdateConfig()
    {
        UnityWebRequest webRequest = new UnityWebRequest("https://remote-config-api.uca.cloud.unity3d.com/configs/98d3ac33-c102-4571-abf3-f9e971cc25ca?projectId=af54b501-0dc8-4a9e-8706-729936028bee", "PUT");
        webRequest.SetRequestHeader("Authorization", "Bearer " + "Aa23Yy_JKmoJzXUa9G8yxouJQHPBigKeM2faJ6eyJrA009f");

        string payload = JsonUtility.ToJson(new Payload(root.configs[0].type, root.configs[0].value));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);

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
            Debug.Log("success");
            //loginData.Invoke(JsonUtility.FromJson<LoginData>(webRequest.downloadHandler.text));
        }
    }
}

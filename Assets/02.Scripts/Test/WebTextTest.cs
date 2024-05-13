using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebTextTest : MonoBehaviour
{
    public Text TextUI;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://time.jsontest.com/");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
            string text = www.downloadHandler.text;
            TextUI.text = text;
            WebTime webtime = JsonUtility.FromJson<WebTime>(text);
            Debug.Log(webtime.date);
            Debug.Log(webtime.milliseconds_since_epoch);
            Debug.Log(webtime.time);
        }
    }
    [Serializable]
    public class WebTime
    {
        public string date;
        public long milliseconds_since_epoch;
        public string time;
    }
}
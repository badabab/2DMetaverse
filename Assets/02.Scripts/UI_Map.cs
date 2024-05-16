using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_Map : MonoBehaviour
{
    public static UI_Map Instance { get; private set; }
    public RawImage MapImageUI;
    public InputField InputFieldUI;
    public List<UI_MapItem> UIMapItems;
    private int _level = 15;
    private string _lastX;
    private string _lastY;

    private GeoResultObject _geoResult;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(GetTexture("127.1054221", "37.3591614"));
    }

    public void Find(Address address)
    {
        StartCoroutine(GetTexture(address.x, address.y));
    }

    IEnumerator GetTexture(string x, string y)
    {
        _lastX = x;
        _lastY = y;

        UnityWebRequest www = UnityWebRequestTexture.GetTexture($"https://naveropenapi.apigw.ntruss.com/map-static/v2/raster?w=700&h=700&center={x},{y}&level={_level}");
        www.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", "rf8p9pqmjg");
        www.SetRequestHeader("X-NCP-APIGW-API-KEY", "ZSINmCIQ6I5t8D9yfHMhDNrUYO9rwrYYHGqK3fkR");

        yield return www.SendWebRequest(); // 비동기

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("성공~");
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            MapImageUI.texture = myTexture;
        }
    }

    public void OnClickResetButton()
    {
        InputFieldUI.text = string.Empty;
    }

    public void OnClickFindButton()
    {
        string location = InputFieldUI.text;

        if (string.IsNullOrEmpty(location))
        {
            return;
        }

        StartCoroutine(MapProcess(location));

    }

    IEnumerator MapProcess(string location)
    {
        _geoResult = null;

        yield return StartCoroutine(GetGeocode(location));

        if (_geoResult == null || _geoResult.addresses.Count == 0)
        {
            yield break;
        }

        StartCoroutine(GetTexture(_geoResult.addresses[0].x, _geoResult.addresses[0].y));


        foreach (var uiMap in UIMapItems)
        {
            uiMap.gameObject.SetActive(false);
        }

        int count = Math.Min(_geoResult.addresses.Count, UIMapItems.Count);
        for (int i = 0; i < count; ++i)
        {
            UIMapItems[i].gameObject.SetActive(true);
            UIMapItems[i].Init(_geoResult.addresses[i]);

        }
    }

    IEnumerator GetGeocode(string location)
    {
        UnityWebRequest www = UnityWebRequest.Get($"https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode?query={location}");
        www.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", "rf8p9pqmjg");
        www.SetRequestHeader("X-NCP-APIGW-API-KEY", "ZSINmCIQ6I5t8D9yfHMhDNrUYO9rwrYYHGqK3fkR");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonText = www.downloadHandler.text;

            _geoResult = JsonUtility.FromJson<GeoResultObject>(jsonText);

        }
    }

    public void OnClickZoomInButton()
    {
        _level++;
        _level = Math.Min(_level, 20);

        StartCoroutine(GetTexture(_lastX, _lastY));
    }

    public void OnClickZoomOutButton()
    {
        _level--;
        _level = Math.Max(_level, 1);

        StartCoroutine(GetTexture(_lastX, _lastY));
    }
}

// ChatGPT에서 가져옴
[Serializable]
public class GeoResultObject
{
    public string status;
    public Meta meta;
    public List<Address> addresses;
    public string errorMessage;
}

[Serializable]
public class Meta
{
    public int totalCount;
    public int page;
    public int count;
}

[Serializable]
public class Address
{
    public string roadAddress;
    public string jibunAddress;
    public string englishAddress;
    public List<AddressElement> addressElements;
    public string x;
    public string y;
    public float distance;
}

[Serializable]
public class AddressElement
{
    public List<string> types;
    public string longName;
    public string shortName;
    public string code;
}
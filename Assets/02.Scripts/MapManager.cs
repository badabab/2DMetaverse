using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("맵 정보 입력")]
    public string latitude = "";
    public string longitude = "";
    public int level = 14;
    public int mapWidth;
    public int mapHeight;


    // Start is called before the first frame update
    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
        StartCoroutine(MapLoader());
    }
    
    IEnumerator MapLoader()
    {
        string str = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster" + "?w=" + mapWidth.ToString() + "&h=" + mapHeight.ToString() + "&center=" + longitude + "," + latitude + "&level=" + level.ToString();

        Debug.Log(str.ToString());

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", "w0bp4qoea6");
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", "bm4r9KObnv4FUKTrdbV8GSHUMu6DJVwSOhOPv1Jg");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }
}
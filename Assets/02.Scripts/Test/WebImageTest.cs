using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebImageTest : MonoBehaviour
{
    public RawImage ImageUI;

    // HTTP: 웹에서 요청(Request)과 응답(Response)을 하기 위한 약속된 형태의 텍스트
    // 웹(WEB): 거미줄이라는 뜻으로 현재는 '인터넷'을 의미

    void Start()
    {
        // 네트워크를 통해 데이터를 받아오는 것은 실시간이 아니기 때문에
        // 코루틴을 이용해서 비동기로 데이터를 받아온다.
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        // Http 주문을 위해 주문서(Request)를 만든다.
        // -> 주문서 내용: URL로부터 텍스쳐(이미지)를 다운로드하기 위한 GET Request 요청
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://www.fitpetmall.com/wp-content/uploads/2023/10/shutterstock_1275055966-1.png");
        yield return www.SendWebRequest();  // 비동기

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            ImageUI.texture = myTexture;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    private static Dictionary<string, Texture> _cache = new Dictionary<string, Texture>();

    public RawImage ProfileImageUI;   // 프로필 이미지
    public TextMeshProUGUI NameTextUI;       // 글쓴이
    public TextMeshProUGUI ContentTextUI;    // 글 내용
    public TextMeshProUGUI LikeTextUI;       // 좋아요 개수
    public TextMeshProUGUI WriteTimeUI;      // 글 쓴 날짜/시간

    public UI_ArticleMenu MenuUI;
    private Article _article;

    public void Init(in Article article)
    {
        _article = article;
        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"좋아요 {article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());
        StartCoroutine(GetTexture(_article.Profile));
    }

    private IEnumerator GetTexture(string url)
    {
        if (url == null)
        {
            url = "http://192.168.200.105:1234/empty.png";
        }

        // 캐쉬된게 있을 때 -> 캐시 히트(적중)
        if (_cache.ContainsKey(url))
        {
            var now = DateTime.Now;

            ProfileImageUI.texture = _cache[url];

            var span = DateTime.Now - now;
            //Debug.Log($"캐시 히트!: {span.TotalMilliseconds}");

            yield break;
        }

        // Http 주문을 위해 주w문서(Request)를 만든다.
        // -> 주문서 내용: URL로부터 텍스처(이미지)를 다운로드하기 위한 GET Request 요청
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        yield return www.SendWebRequest(); // 비동기

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            ProfileImageUI.texture = myTexture;

            stopwatch.Stop();
            //Debug.Log($"캐시 미스!: {stopwatch.ElapsedTicks}"); // 나노세컨즈

            // 캐싱
            _cache[url] = myTexture;
        }
    }
    
    private string GetTimeString(DateTime dateTime)
    {
        TimeSpan time = DateTime.Now - dateTime;    
        if (time.TotalMinutes < 1)      // 1분 이내 -> 방금 전
        {
            return "방금 전";
        }
        else if (time.TotalHours < 1 && time.Days == 0)     // 1시간 이내 -> n분 전
        {
            return $"{time.Minutes:N0}분 전";
        }
        else if (time.TotalDays < 1 && time.Days == 0)      // 하루 이내 -> n시간 전
        {
            return $"{time.TotalHours:N0}시간 전";
        }
        else if (time.TotalDays < 7)        // 7일 이내 -> n일 전
        {
            return $"{time.TotalDays:N0}일 전";
        }
        else if (time.TotalDays < 7 * 4)    // 4주 이내 -> n주 전
        {
            return $"{time.TotalDays / 7:N0}주 전";
        }
        return dateTime.ToString("yyyy년 M월 d일");
    }

    public void OnClickMenuButton()
    {
        MenuUI.Show(_article);
    }

    public void OnClickLikeButton()
    {
        ArticleManager.Instance.AddLike(_article);
        ArticleManager.Instance.FindAll();
        UI_ArticleList.Instance.Show();
    }
}
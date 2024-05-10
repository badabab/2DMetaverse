using MongoDB.Bson;
using MongoDB.Driver;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    public Image ProfileImageUI;   // 프로필 이미지
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
        LikeTextUI.text = $"{article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());
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
        ArticleManager.Instance.Like(_article);
        ArticleManager.Instance.FindAll();
        UI_ArticleList.Instance.Show();
    }
}
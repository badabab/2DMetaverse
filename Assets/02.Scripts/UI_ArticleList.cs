using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;

// UI_Article 관리
public class UI_ArticleList : MonoBehaviour
{
    public static UI_ArticleList Instance { get; private set; }

    public List<UI_Article> UIArticles;
    public GameObject EmptyObject;
    public GameObject UI_ArticleWrite;

    public TextMeshProUGUI Button_All;
    public TextMeshProUGUI Button_Notice;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Refresh();
        UI_ArticleWrite.SetActive(false);
    }

    // 새로고침
    public void Refresh()
    {
        // 1. Article매니저로부터 Article을 가져온다.
        List<Article> articles = ArticleManager.Instance.Articles;

        // 게시글의 개수가 0개일 때만 '첫 글을 작성해보세요' 보여주기
        EmptyObject.gameObject.SetActive(articles.Count == 0);

        // 2. 모든 UI_Article을 끈다.
        foreach (UI_Article uiArticle in UIArticles)
        {
            uiArticle.gameObject.SetActive(false);
        }
          
        for (int i = 0; i < articles.Count && i < UIArticles.Count; i++)
        {
            // 3. 가져온 Article 개수만큼 UI_Article을 킨다.    
            UIArticles[i].gameObject.SetActive(true);

            // 4. 각 UI_Article의 내용을 Article로 초기화(Init)한다.
            UIArticles[i].Init(articles[i]);
        }
    }

    public void OnClickAllButton()
    {
        ArticleManager.Instance.FindAll();
        Refresh();
        Button_All.color = new Color32(0, 0, 0, 255);
        Button_Notice.color = new Color32(0, 0, 0, 150);
    }

    public void OnClickNoticeButton()
    {
        ArticleManager.Instance.FindNotice();
        Refresh();
        Button_All.color = new Color32(0, 0, 0, 150);
        Button_Notice.color = new Color32(0, 0, 0, 255);
    }

    public void OnClickWriteButton()
    {
        Button_All.color = new Color32(0, 0, 0, 255);
        Button_Notice.color = new Color32(0, 0, 0, 150);
        UI_ArticleWrite.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Refresh();
    }
}
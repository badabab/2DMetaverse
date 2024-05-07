using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI_Article 관리
public class UI_ArticleList : MonoBehaviour
{
    public List<UI_Article> UIArticles;
    public GameObject EmptyObject;

    private void Start()
    {
        Refresh();
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
}
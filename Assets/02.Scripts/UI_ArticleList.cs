using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI_Article 관리
public class UI_ArticleList : MonoBehaviour
{
    public List<UI_Article> UIArticles;

    private void Start()
    {
        Refresh();
    }

    // 새로고침
    public void Refresh()
    {
        // 1. Article매니저로부터 Article을 가져온다.
        List<Article> articles = ArticleManager.Instance.Articles;

        // 2. 모든 UI_Article을 끈다.
        foreach (UI_Article article in UIArticles)
        {
            article.gameObject.SetActive(false);
        }

        // 3. 가져온 Article 개수만큼 UI_Article을 킨다.        
        for (int i = 0; i < articles.Count && i < UIArticles.Count; i++)
        {
            UIArticles[i].gameObject.SetActive(true);

            // 4. 각 UI_Article의 내용을 Article로 초기화(Init)한다.
            UIArticles[i].Init(articles[i]);
        }
    }
}
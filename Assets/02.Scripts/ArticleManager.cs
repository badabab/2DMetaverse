using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 하나만을 보장
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    // 게시글 리스트
    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    public static ArticleManager Instance {  get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _articles.Add(new Article()
        {
            Name = "김홍일",
            Content = "안녕하세요.",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "민예진",
            Content = "하이",
            ArticleType = ArticleType.Normal,
            Like = 7,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "조희수",
            Content = "해삐:)",
            ArticleType = ArticleType.Normal,
            Like = 908,
            WriteTime = DateTime.Now

        });
        _articles.Add(new Article()
        {
            Name = "고승연",
            Content = "안녕하세~",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "이태환",
            Content = "나는 전설이다.",
            ArticleType = ArticleType.Normal,
            Like = 320,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "이성민",
            Content = "나는 짱이다.",
            ArticleType = ArticleType.Normal,
            Like = 30,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "96년생정성훈",
            Content = "하이루방가방가",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
    }
}

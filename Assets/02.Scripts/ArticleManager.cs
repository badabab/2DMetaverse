using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

// 1. 하나만을 보장
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    // 게시글 리스트
    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    public UI_ArticleList UI_ArticleList;

    public static ArticleManager Instance {  get; private set; }
    private void Awake()
    {
        Instance = this;

        RefreshList();
    }

    public void OnClickNoticeButton()
    {
        UI_ArticleList.Refresh();
        _articles.Clear();

        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase metaverseDB = mongoClient.GetDatabase("metaverse");
        IMongoCollection<BsonDocument> articlesCollection = metaverseDB.GetCollection<BsonDocument>("articles");
        var noticeFilter = Builders<BsonDocument>.Filter.Eq("ArticleType", 1);
        List<BsonDocument> documents = articlesCollection.Find(noticeFilter).ToList();
        foreach (var data in documents)
        {
            Article article = new Article();
            article.ArticleType = (ArticleType)(int)data["ArticleType"];
            article.Name = data["Name"].ToString();
            article.Content = data["Content"].ToString();
            article.Like = (int)data["Like"];
            article.WriteTime = DateTime.Parse(data["WriteTime"].AsString);

            // _articles에 넣기
            _articles.Add(article);
        }
    }

    public void OnClickAllButton()
    {
        UI_ArticleList.Refresh();
        _articles.Clear();

        RefreshList();
    }

    private void RefreshList()
    {
        // 몽고 DB로부터 article 조회

        // 1. 몽고DB 연결
        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);

        // 2. 특정 데이터베이스 연결
        IMongoDatabase metaverseDB = mongoClient.GetDatabase("metaverse");

        // 3. 특정 콜렉션 연결
        IMongoCollection<BsonDocument> articlesCollection = metaverseDB.GetCollection<BsonDocument>("articles");

        // 4. 모든 문서 읽어오기
        List<BsonDocument> documents = articlesCollection.Find(new BsonDocument()).ToList();

        // 5. 읽어 온 문서 만큼 New Article()에서 데이터 채우고 _articles에 넣기
        foreach (var data in documents)
        {
            Article article = new Article();
            article.ArticleType = (ArticleType)(int)data["ArticleType"];
            article.Name = data["Name"].ToString();
            article.Content = data["Content"].ToString();
            article.Like = (int)data["Like"];
            article.WriteTime = DateTime.Parse(data["WriteTime"].AsString);

            // _articles에 넣기
            _articles.Add(article);
        }
    }
}
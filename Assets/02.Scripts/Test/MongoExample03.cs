using MongoDB.Driver;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample03 : MonoBehaviour
{
    // 도큐먼트 삽입 (Create)
    private void Start()
    {
        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        IMongoCollection<Article> collection = sampleDB.GetCollection<Article>("articles");

        // 1. 도큐먼트 하나 삽입
        // InsertOne(도큐먼트)
        Article article = new Article
        {
            Name = "공지수",
            Content = "공지사항입니다.",
            ArticleType = ArticleType.Notice,
            Like = 100,
            WriteTime = DateTime.Now
        };
        Debug.Log(article.Id);          // 000000000000000000000000
        collection.InsertOne(article);
        Debug.Log(article.Id);          // 663d7b2c836689cd70d307dc

        // 2. 도큐먼트 여러 개 삽입
        // InsertMany(List<도큐먼트>)
        List<Article> articles = new List<Article>
        {
            new Article()
            {
                Name = "티모",
                Content = "민성씨 빨리와요",
                ArticleType = ArticleType.Notice,
                Like = 100,
                WriteTime = DateTime.Now
            },
            new Article()
            {
                Name = "말파이트",
                Content = "성민씨 휴가 왜 쓰셨나요",
                ArticleType = ArticleType.Notice,
                Like = 100,
                WriteTime = DateTime.Now
            },
            new Article()
            {
                Name = "볼꾸냥",
                Content = "경희씨 아프지마세요",
                ArticleType = ArticleType.Notice,
                Like = 100,
                WriteTime = DateTime.Now
            },
        };
        collection.InsertMany(articles);
    }
}

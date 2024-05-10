using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

// 1. 하나만을 보장
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    // 게시글 리스트
    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    // 콜렉션
    private IMongoCollection<Article> _articleCollection;

    public static ArticleManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Init();
        FindAll();
    }

    public void FindAll()
    {
        // 4. 모든 문서 읽어오기
        // 4-1. WriteTime을 기준으로 '정렬'
        // Sort 메서드를 이용해서 도큐먼트를 정렬할 수 있다.
        // 매개변수로는 어떤 Key로 정렬할 것인지 알려주는 BsonDocument를 전달해주면 된다.
        var sort = new BsonDocument();
        sort["WriteTime"] = -1;
        // +1 -> 오름차순 정렬 -> 낮은 값에서 높은 값으로 정렬한다.
        // -1 -> 내림차순 정렬 -> 높은 값에서 낮은 값으로 정렬한다.
        _articles = _articleCollection.Find(new BsonDocument()).Sort(sort).ToList();
        /*_articles.Clear();
        // 5. 읽어 온 문서 만큼 New Article()에서 데이터 채우고 _articles에 넣기
        foreach (var data in dataList)
        {
            Article article = new Article();
            article.ArticleType = (ArticleType)(int)data["ArticleType"];
            article.Name = data["Name"].ToString();
            article.Content = data["Content"].ToString();
            article.Like = (int)data["Like"];
            article.WriteTime = DateTime.Parse(data["WriteTime"].ToString());
            // _articles에 넣기
            _articles.Add(article);
        }*/
    }

    public void FindNotice()
    {
        _articles = _articleCollection.Find(data => (int)data.ArticleType == (int)ArticleType.Notice).ToList();
       /* _articles.Clear();
        foreach (var data in documents)
        {
            Article article = new Article();
            article.ArticleType = (ArticleType)(int)data["ArticleType"];
            article.Name = data["Name"].ToString();
            article.Content = data["Content"].ToString();
            article.Like = (int)data["Like"];
            article.WriteTime = DateTime.Parse(data["WriteTime"].ToString());
            // _articles에 넣기
            _articles.Add(article);
        }*/
    }   

    private void Init()
    {
        // 몽고 DB로부터 article 조회
        // 1. 몽고DB 연결
        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);

        // 2. 특정 데이터베이스 연결
        IMongoDatabase db = mongoClient.GetDatabase("metaverse");

        // 3. 특정 콜렉션 연결
        // _articleCollection = db.GetCollection<BsonDocument>("articles");
        _articleCollection = db.GetCollection<Article>("articles");
    }

    public void Write(ArticleType articleType, string content)
    {
        Article article = new Article()
        {
            Name = "고양이",
            Content = content,
            ArticleType = articleType,
            Like = 0,
            WriteTime = DateTime.Now
        };
        _articleCollection.InsertOne(article);
        FindAll();
    }

    public void Delete(ObjectId id)
    {       
        /*var filter = Builders<Article>.Filter.Eq("_id", id);
        _articleCollection.DeleteOne(filter);*/
        _articleCollection.DeleteOne(d => d.Id == id);
    }

    public void Replace(Article article)
    {
        _articleCollection.ReplaceOne(d => d.Id == article.Id, article);
    }

    public void Like(Article article)
    {
        var updateDefinition = Builders<Article>.Update.Inc("Like", 1);
        UpdateResult result = _articleCollection.UpdateMany(d => d.Id == article.Id, updateDefinition);
    }
}
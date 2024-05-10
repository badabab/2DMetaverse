using MongoDB.Driver;
using UnityEngine;

public class MongoExample04 : MonoBehaviour
{
    private void Start()
    {
        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        IMongoCollection<Article> collection = sampleDB.GetCollection<Article>("articles");

        // 도큐먼트 수정
        // 1. Update: 특정 도큐먼트의 Value를 수정
        // 2. Replace: 특정 도큐먼트의 id를 제외하고 전부 대체

        // 1-1. UpdateOne, UpdateMany
        // UpdateOne(filter, updateDefinition)
        // filter: 업데이트 할 도큐먼트의 기준
        // updateDefinition: 도큐먼트에 적용할 변동사항
        var updateDefinition = Builders<Article>.Update.Set("Name", "공지수");
        UpdateResult result = collection.UpdateMany(data => data.Name == "티모", updateDefinition);
        Debug.Log($"수정된 문서 개수: {result.ModifiedCount}");

        // 2-1. ReplaceOne, ReplaceMany
        // 도큐먼트 교체
        Article article = collection.Find(d => d.Name == "공지수").First();
        article.Content = "나는 바보다";
        ReplaceOneResult result2 = collection.ReplaceOne(d => d.Id == article.Id, article);
        Debug.Log(result2.ModifiedCount);

        // 도큐먼트 삭제
        // DeleteOne, DeleteMany
        // 필터 - 삭제할 문서의 기준
        DeleteResult deleteResult = collection.DeleteOne(d => d.Name == "공지수");
        Debug.Log($"{deleteResult.DeletedCount}개의 문서가 삭제되었습니다.");

        collection.DeleteOne(d => d.Id == article.Id);
    }
}
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample02 : MonoBehaviour
{
    void Start()
    {
        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("sample_mflix");
        IMongoCollection<BsonDocument> movieCollection = sampleDB.GetCollection<BsonDocument>("movies");

        // SQL: Structured(Select) Query(질의) Language
        // 읽기, 쓰기, 수정, 삭제
        // 읽기: Find 메서드
        // Finds 메서드: 컬렉션 담겨 있는 도큐먼트를 조회하는 메서드다.
        // 사용 빈도가 압도적으로 높다. -> 그만큼 중요하다!
        
        // 1. 도큐먼트 하나 읽기
        BsonDocument data = movieCollection.Find(new BsonDocument()).First();
        Debug.Log(data["plot"]);

        // BsonDocument 는 (binary) json을 표현하는 자료형이다.
        // new BsonDocument(); --> { }

        // 2. 도큐먼트 10개 읽기
        List<BsonDocument>  datas = movieCollection.Find(new BsonDocument()).Limit(10).ToList();
        foreach (var d in datas)
        {
            Debug.Log(d["title"]);
        }

        // 3. 비교 연산자: 2002년도에 개봉한 영화 5개 찾기
        // 3-1. Bson '값으로 쿼리'
        BsonDocument filter = new BsonDocument();
        filter["year"] = 2002;
        List<BsonDocument> data2002 = movieCollection.Find(filter).Limit(5).ToList();

        // 3-2. '필터 쿼리'를 사용한 방식
        var filter2 = Builders<BsonDocument>.Filter.Eq("year", 2002);
        List<BsonDocument> data20022 = movieCollection.Find(filter2).Limit(5).ToList();

        // 4. 논리 연산자(And, Or, Not)
        // if(1992 <= year && year <= 2002)
        var filterGte1992 = Builders<BsonDocument>.Filter.Gte("year", 1992);
        var filterLte2002 = Builders<BsonDocument>.Filter.Lte("year", 2002);
        var filterFinal   = Builders<BsonDocument>.Filter.And(filterGte1992, filterLte2002);
        var data1992_2002 = movieCollection.Find(filterFinal).Limit(5).ToList();
        foreach (var d in data1992_2002)
        {
            Debug.Log(d["title"]);
        }

        // 5. where 연산자
        var wherefilter = Builders<BsonDocument>.Filter.Where(d => 1992 <= d["year"] && d["year"] <= 2002);
        var data1992_20022 = movieCollection.Find(wherefilter).Limit(5).ToList();
        foreach (var d in data1992_20022)
        {
            Debug.Log(d["title"]);
        }

        // 6. 람다식
        var data1992_20023 = movieCollection.Find(d => 1992 <= d["year"] && d["year"] <= 2002).Limit(5).ToList();
        foreach (var d in data1992_20023)
        {
            Debug.Log(d["title"]);
        }
    }
}
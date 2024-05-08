using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class MongoExample01 : MonoBehaviour
{
    private void Start()
    {
        // 몽고 데이터베이스에 연결
        // 연결 문자열: 데이터베이스 연결을 위한 정보가 담겨있는 문자열
        string connectionString = "mongodb+srv://mongodb:mongodb@cluster0.2rt9iau.mongodb.net/";
        // - 프로토콜: mongodb + srv
        // - 아이디: mongodb
        // - 비밀번호: mongodb
        // - 주소: cluster0.2rt9iau.mongodb.net

        // 1. 접속
        MongoClient mongoClient = new MongoClient(connectionString);
        
        // 2. 데이터베이스가 뭐뭐있지? -> 데이터베이스 조회
        List<BsonDocument> dbList = mongoClient.ListDatabases().ToList();
        foreach (BsonDocument db in dbList)
        {
            Debug.Log(db["name"]);
        }

        // 3. 특정 데이터베이스에 연결
        IMongoDatabase sampleDB = mongoClient.GetDatabase("sample_mflix");

        // 4. 콜렉션들 이름 확인
        List<string> collectionNames = sampleDB.ListCollectionNames().ToList();
        foreach (string name in collectionNames)
        {
            Debug.Log(name);
        }

        // 5. 콜렉션 연결 및 도큐먼트 개수 출력
        //IMongoCollection<BsonDocument> movieCollection = sampleDB.GetCollection<BsonDocument>("movies");
        var movieCollection = sampleDB.GetCollection<BsonDocument>("movies");
        long count = movieCollection.CountDocuments(new BsonDocument());
        Debug.Log($"영화 개수: {count}");

        // 6. 도큐먼트 하나만 읽기
        BsonDocument firstDoc = movieCollection.Find(new BsonDocument()).FirstOrDefault();
        Debug.Log(firstDoc["plot"]);
    }
}
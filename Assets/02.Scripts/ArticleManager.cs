using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        /* _articles.Add(new Article()
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
         });*/


        // JSON이란 자바스크립트 객체 표기법으로 요즘 가장 많이 사용하는
        //         데이터 텍스트 구조
        // C#의 딕셔너리를 표현하는 방법과 비슷하다.
        // "키":"밸류" 형태의 데이터를 객체({})와 배열([])의 조합으로 표현한다.

        // 유니티의 특별한 파일 저장 경로
        // 유니티만이 쓸 수 있는 파일 저장 경로를 가지고 있다.
        Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath;

        // 1. 객체를 Json포맷의 텍스르로 변환한 다음 파일 'data.txt'에 저장한다.
        // json은 일반 클래스는 직렬화할 수 있지만 리스트 그 자체는 직렬화를 못한다.
        // 일반 클래스로 리스트를 덮어 씌운다.
        /*ArticleData articleData = new ArticleData(_articles);
        string jsonData = JsonUtility.ToJson(articleData);
        Debug.Log(jsonData);
        StreamWriter sw = File.CreateText($"{path}/data.txt");
        sw.Write(jsonData);
        sw.Close();*/

        // 2. 데이터를 하드코딩한 코드를 지운다.
        // 3. 'data.txt'로부터 json을 읽어와서 객체들을 초기화한다.
        string txt = File.ReadAllText($"{path}/data.txt");
        _articles = JsonUtility.FromJson<ArticleData>(txt).Data;
    }
}
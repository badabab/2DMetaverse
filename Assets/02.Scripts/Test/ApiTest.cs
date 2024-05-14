using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class APISearch : MonoBehaviour
{
    public List<Product> products;
    public RawImage image;
    IEnumerator Start()
    {
        string query = "기계식키보드"; // 검색할 문자열
        string url = "https://openapi.naver.com/v1/search/shop.json?query=" + query; // JSON 결과

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("X-Naver-Client-Id", "_PDE96wWg4eTrGDe345h"); // 클라이언트 아이디
        request.SetRequestHeader("X-Naver-Client-Secret", "jflvKKcC6B"); // 클라이언트 시크릿

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string text = request.downloadHandler.text;
            //Debug.Log(text);

            // JSON을 파싱하여 객체로 변환
            ProductList productList = JsonUtility.FromJson<ProductList>(text);

            // 리스트에 추가
            products = new List<Product>(productList.items);
            
            // 결과 확인
            foreach (Product product in products)
            {
                Debug.Log("Title: " + product.title);
                Debug.Log("Link: " + product.link);
                Debug.Log("Image: " + product.image);
                Debug.Log("LPrice: " + product.lprice);
                Debug.Log("HPrice: " + product.hprice);
                Debug.Log("MallName: " + product.mallName);
                Debug.Log("ProductID: " + product.productId);
                Debug.Log("ProductType: " + product.productType);
                Debug.Log("Brand: " + product.brand);
                Debug.Log("Maker: " + product.maker);
                Debug.Log("Category1: " + product.category1);
                Debug.Log("Category2: " + product.category2);
                Debug.Log("Category3: " + product.category3);
                Debug.Log("Category4: " + product.category4);
                Debug.Log("\n");

                UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(product.image);
                yield return request2.SendWebRequest();
                Texture2D texture = ((DownloadHandlerTexture)request2.downloadHandler).texture;
                image.texture = texture;
            }
        }
        else
        {
            Debug.Log("Error 발생=" + request.error);
        }
    }

    [Serializable]
    public class Product
    {
        public string title;
        public string link;
        public string image;
        public string lprice;
        public string hprice;
        public string mallName;
        public string productId;
        public string productType;
        public string brand;
        public string maker;
        public string category1;
        public string category2;
        public string category3;
        public string category4;
    }
    [Serializable]
    public class ProductList
    {
        public List<Product> items;
    }
}
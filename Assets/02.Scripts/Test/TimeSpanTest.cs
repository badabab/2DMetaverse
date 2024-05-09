using System;
using UnityEngine;

public class TimeSpanTest : MonoBehaviour
{
    private void Start()
    {
        // TimeSpan은 시간 간격을 나타내는 클래스
        // 시간을 조작하거나 두 날짜 사이의 차이를 계산할 수 있다.
        TimeSpan twoHour = new TimeSpan(2, 0, 0);
        Debug.Log(twoHour);
        Debug.Log(twoHour.TotalMinutes);    // 2시간이 몇분이지?
        Debug.Log(twoHour.TotalSeconds);    // 2시간이 몇초지?

        // 크리스마스까지 얼마나 남았지?
        DateTime xMasDate = new DateTime(DateTime.Today.Year, 12, 25);
        DateTime today = DateTime.Today;
        // DateTime에 대한 사칙연산은 '빼기'만 된다.
        TimeSpan diff = xMasDate - today;
        Debug.Log($"크리스마스까지 남은 일은 {diff.TotalDays}입니다.");

        // 2024.2.26을 기준으로
        // 100일이 언제고, 몇 일 남았는지 계산해서 출력
        DateTime loveDay = new DateTime(2024, 2, 26);
        DateTime hundredDaysLater = loveDay.AddDays(100 - 1);
        Debug.Log(hundredDaysLater.ToString("100일은 MM/dd(ddd요일)이다."));
        TimeSpan leftTime = hundredDaysLater - DateTime.Today;
        Debug.Log($"100일까지 남은 일: {leftTime.TotalDays}일");

        Debug.Log(DateTime.Today);  // 05/09/2024 00:00:00
        // 2024/5/9 11:36:05
        // ISO 시간표기법
        string intNumber = "324";
        int number = Int32.Parse(intNumber);

        string stringDate = "2024-05-07T14:32:17";
        DateTime date = DateTime.Parse(stringDate);
    }
}
using System;
using UnityEngine;

public class DataTimeTest : MonoBehaviour
{
    // Math -> 수학
    // Vector3 -> 위치와 방향
    // DataTime -> 날짜와 시간을 표현하는 클래스
    // 생성, 조작, 비교

    private void Start()
    {
        // 1. 생성
        // [year, month, day,] hour, minute, second, millisecond
        DateTime birth = new DateTime(1990, 7, 28, 09, 00, 00);
        Debug.Log(birth);
        // 오늘 날짜
        DateTime today = DateTime.Today;
        Debug.Log(today);
        // 지금
        DateTime now = DateTime.Now;
        Debug.Log(now);

        // 2. 조작
        DateTime tomorrow = DateTime.Today.AddDays(1);      // 내일
        Debug.Log(tomorrow);    
        DateTime lastWeek = DateTime.Today.AddDays(-7);     // 지난주
        Debug.Log(lastWeek);

        // 3. 속성
        Debug.Log(tomorrow.Year);
        Debug.Log(tomorrow.Month);
        Debug.Log(tomorrow.Day);
        Debug.Log(tomorrow.Hour);
        Debug.Log(tomorrow.Minute);
        Debug.Log(tomorrow.Second);
        Debug.Log(tomorrow.DayOfWeek);

        // 4. 비교
        DateTime person1Birth = new DateTime(2000, 1, 1);
        DateTime person2Birth = new DateTime(2002, 5, 31);
        Debug.Log(person1Birth > person2Birth);

        Debug.Log($"{tomorrow.Year}년 {tomorrow.Month}월 {tomorrow.Day}일은 {tomorrow.DayOfWeek}입니다.");
        // 5. 표현 형식
        Debug.Log(tomorrow.ToString("yyyy년 MM월 dd일 ddd요일 입니다."));

        // UTC: 협정 세계 시간 (시간의 기준)
        // 영국의 그리니치 천문대가 위치한 시간을 기준으로 시차를 표기하는 방법
        // UTC: 세계 표준 시간
        // Local: 자기 지역 시간
        // DateTime은 디바이스의 설정에 근거해서 데이터를 초기화한다.
        DateTime now2 = DateTime.Now;
        Debug.Log(now2.Kind);       // Local
        DateTime utcNow = DateTime.UtcNow;  // 한국: UTC 9
        Debug.Log(utcNow);
        Debug.Log(utcNow.Kind);     // Utc
    }
}
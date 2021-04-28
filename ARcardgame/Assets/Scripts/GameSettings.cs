using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scene 전체에서 쓰일 게임 세팅 static 변수 저장
public static class GameSettings
{
    public static int order = -1; //0이면 플레이어 차례인 상태, 1이면 컴퓨터 차례인 상태
    public static bool canCardDivide = false;

    public static int playerChips = 20; //플레이어 남은 칩 수
    public static int comChips = 20; //com 남은 칩 수

    public static bool gameOver = false; //다이, 베팅 끝 등 게임이 끝난 상태인지
    //public static string 
}

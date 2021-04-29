using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager; //다른 script에서 GameManager.manager로 접근하면 변수, 메소드 사용 가능 

    public int orderNum = 3; //0이면 player 차례인 상태, 1이면 com 차례인 상태
    public bool canCardDivide = false;

    public bool activate = false; //일정 텍스트 출력 후 베팅 또는 다이 선택을 가능하게 하는 상태 - S2ProcessText와 SpawnObjects 등에서 관리

    public int playerChips = 20;
    public int comChips = 20;

    public int comBets = 5;
    public int playerBets = 0;

    public GameState currentGameState = GameState.main;
    public int currentComState = 0; //베팅하려는 상태 0, die를 선택할시 1
    public int currentPlayerState = 0; //0이 디폴트, die 선택할시 1, 현재 acting 중일 시 2

    private Play2UI p2UI;

    public enum GameState
    {
        //게임 상태 enum, 필요한 상태가 더 있으면 추가해주세요.
        main,
        option,
        inGame,
        gameOver
    }
    // Start is called before the first frame update
    void Awake()
    {
        manager = this; 
    }

    private void Start()
    {
        Debug.Log("manager starts");
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            //Debug.Log("activated");
        }
    }
    public void StartGame()
    {
        SetGameState(GameState.inGame); //불러오는 시점에 "게임중" 상태로 만듦
    }

    void SetGameState(GameState state)
    {
        //게임 상태를 결정하는 메소드, 게임 오버 상황 시 이걸로 state 변경하시면 됩니다!
        if (state == GameState.main) { }
        else if (state == GameState.option) { }
        else if (state == GameState.inGame) { }
        else if (state == GameState.gameOver) { }

        currentGameState = state;
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    void RestartGame()
    {

    }

    public void PlayerAct()
    {
        currentPlayerState = 2;
        p2UI = FindObjectOfType<Canvas>().GetComponent<Play2UI>();
        p2UI.dieButton.interactable = true;
        p2UI.okButton.interactable = true;
    }


    public void ComAI()
    {
        //일단 AI 구축이 안 되어있지만 UI 출력을 위해 랜덤으로 int를 받아서 베팅할지 다이할지만 선택하게 했습니다.
        int ran = Random.Range(0, 2); //0이면 베팅, 1이면 다이

        switch (ran)
        {
            case 0:
                currentComState = 0;
                break;
            case 1:
                currentComState = 1;
                break;
        }

        if(currentComState == 1)
        {
            return;
        }
        
        if(currentComState == 0 && orderNum == 0)
        {
            comBets = 5;//일단 5개
            comChips -= comBets; //베팅한 만큼 가진 칩에서 제거
        }

    }
}

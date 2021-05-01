using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //게임 내 필요 정보를 저장하는 곳입니다. static 변수로 선언되어있어 어디서나 사용가능하게 합니다.

    public static GameManager manager; //다른 script에서 GameManager.manager로 접근하면 변수, 메소드 사용 가능 

    public int orderNum = 3; //0이면 player 차례인 상태, 1이면 com 차례인 상태
    public bool canCardDivide = false;

    public bool activate = false; //일정 텍스트 출력 후 베팅 또는 다이 선택을 가능하게 하는 상태 - S2ProcessText와 SpawnObjects 등에서 관리

    public int playerChips = 20;
    public int comChips = 20;

    public int comBets = 5;
    public int playerBets = 0;

    public GameState currentGameState = GameState.main;

    public int currentComState = 0; //0이 디폴트, die를 선택할시 1, 현재 acting 중일 시 2
    public int currentPlayerState = 0; //0이 디폴트, die 선택할시 1, 현재 acting 중일 시 2

    private Play2UI p2UI;
    private SpawnObjects spObjects;

    public string comCard;
    public int comCardNum;
    public int playerCardNum;

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
        if(currentComState == 1)
        {

        }

        if(currentPlayerState == 1)
        {

        }

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
        activate = false;
    }

    void RestartGame()
    {

    }
    void DividePlayerCard()
    {
        while (true)
        {
            playerCardNum = Random.Range(0, 10);

            if (playerCardNum != comCardNum)
                break;
        }
    }

    void makeCardNameToInteger()
    {
        if (comCard.Equals("AceofDiamonds (Instance)"))
        {
            comCardNum = 0;
        }
        else if (comCard.Equals("2ofDiamonds (Instance)"))
        {
            comCardNum = 1;
        }
        else if (comCard.Equals("3ofDiamonds (Instance)"))
        {
            comCardNum = 2;
        }
        else if (comCard.Equals("4ofDiamonds (Instance)"))
        {
            comCardNum = 3;
        }
        else if (comCard.Equals("5ofDiamonds (Instance)"))
        {
            comCardNum = 4;
        }
        else if (comCard.Equals("6ofDiamonds (Instance)"))
        {
            comCardNum = 5;
        }
        else if (comCard.Equals("7ofDiamonds (Instance)"))
        {
            comCardNum = 6;
        }
        else if (comCard.Equals("8ofDiamonds (Instance)"))
        {
            comCardNum = 7;
        }
        else if (comCard.Equals("9ofDiamonds (Instance)"))
        {
            comCardNum = 8;
        }
        else if (comCard.Equals("10ofDiamonds (Instance)"))
        {
            comCardNum = 9;
        }
    }

    public void PlayerAct()
    {
        currentPlayerState = 2;
        p2UI = FindObjectOfType<Canvas>().GetComponent<Play2UI>();
        p2UI.dieButton.interactable = true;
        p2UI.okButton.interactable = true;
    }

    public void TurnEnds()
    {
        if (orderNum == 0 && currentPlayerState == 2)
        {
            playerChips -= playerBets;
            p2UI.UpdatePText(playerChips);
            currentPlayerState = 0;
            orderNum = 1;
            activate = false;
            playerBets = 0; //각종 값들 초기화
        }
        else if (orderNum == 1 && currentComState == 2)
        {
            p2UI.UpdateComText(comChips);
            currentComState = 0;
            orderNum = 0;
            activate = false;
        }
    }

    public void ComAI()
    {
        spObjects = GameObject.Find("Indicator").GetComponent<SpawnObjects>();
        //일단 AI 구축이 안 되어있지만 UI 출력을 위해 랜덤으로 int를 받아서 베팅할지 다이할지만 선택하게 했습니다.
        //int ran = Random.Range(0, 2); //0이면 베팅, 1이면 다이
        int ran = 0;

        switch (ran)
        {
            case 0:
                currentComState = 2;
                break;
            case 1:
                currentComState = 1;
                break;
        }

        if(currentComState == 1)
        {
            return;

        }else if(currentComState == 2 && orderNum == 1)
        {
            comBets = 5;//일단 5개
            comChips -= comBets; //베팅한 만큼 가진 칩에서 제거
            Debug.Log("comact call");
            spObjects.ComActs();
        }

    }
}

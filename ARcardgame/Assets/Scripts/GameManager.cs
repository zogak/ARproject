using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //���� �� �ʿ� ������ �����ϴ� ���Դϴ�. static ������ ����Ǿ��־� ��𼭳� ��밡���ϰ� �մϴ�.

    public static GameManager manager; //�ٸ� script���� GameManager.manager�� �����ϸ� ����, �޼ҵ� ��� ���� 


    public int orderNum = 3; //0�̸� player ������ ����, 1�̸� com ������ ����
    public bool canCardDivide = false;

    public bool activate = false; //���� �ؽ�Ʈ ��� �� ���� �Ǵ� ���� ������ �����ϰ� �ϴ� ���� - S2ProcessText�� SpawnObjects ��� ����

    public int playerChips = 20;
    public int comChips = 20;

    public int comBets = 0;
    public int playerBets = 0;

    public GameState currentGameState = GameState.main;

    public int currentComState = 0; //0�� ����Ʈ, die�� �����ҽ� 1, ���� acting ���� �� 2
    public int currentPlayerState = 0; //0�� ����Ʈ, die �����ҽ� 1, ���� acting ���� �� 2

    private Play2UI p2UI;
    private SpawnObjects spObjects;

    public string comCard;
    public int comCardNum = -1;
    public int playerCardNum;

    public int howManyComTurn = 0;
    public int howManyPlayerTurn = 0;
    public int totalTurns = 0;

    public enum GameState
    {
        //���� ���� enum, �ʿ��� ���°� �� ������ �߰����ּ���.
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
        SetGameState(GameState.inGame); //�ҷ����� ������ "������" ���·� ����
    }

    public void SetGameState(GameState state)
    {
        //���� ���¸� �����ϴ� �޼ҵ�, ���� ���� ��Ȳ �� �̰ɷ� state �����Ͻø� �˴ϴ�!
        if (state == GameState.main) { }
        else if (state == GameState.option) { }
        else if (state == GameState.inGame) { }
        else if (state == GameState.gameOver) 
        {
            FinalMotion();
        }

        currentGameState = state;
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        activate = false;
    }

    public void FinalMotion()
    {
        spObjects.finalResult();
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

        playerCardNum = Random.Range(0, 10);

        while (playerCardNum == comCardNum)
        {
            playerCardNum = Random.Range(0, 10);
        }

        p2UI = FindObjectOfType<Canvas>().GetComponent<Play2UI>();
        p2UI.dieButton.interactable = true;

    }

    public void PlayerAct()
    {
        currentPlayerState = 2;
        //p2UI = FindObjectOfType<Canvas>().GetComponent<Play2UI>();
        //p2UI.dieButton.interactable = true;
               
        //p2UI.okButton.interactable = true;
        
        
    }

    public void TurnEnds()
    {
        if (orderNum == 0 && currentPlayerState == 2) //player����, ����������
        {
            playerChips -= playerBets;
            p2UI.UpdatePText(playerChips);
            currentPlayerState = 0;
            orderNum = 1;
            activate = false;
            comBets = playerBets;
            playerBets = 0; //���� ���� �ʱ�ȭ
            howManyPlayerTurn++; //playerTurn�� ���°����
        }
        else if (orderNum == 1 && currentComState == 2) //computer����, ����������
        {
            p2UI.UpdateComText(comChips); //null exception
            currentComState = 0;
            orderNum = 0;
            howManyComTurn++; // comTurn�� ���°����
            activate = false;
        }
    }

 

    public void ComAI()
    {
        spObjects = GameObject.Find("Indicator").GetComponent<SpawnObjects>();
        //�ϴ� AI ������ �� �Ǿ������� UI ����� ���� �������� int�� �޾Ƽ� �������� ���������� �����ϰ� �߽��ϴ�.

        //int ran = Random.Range(0, 2); //0�̸� ����, 1�̸� ����
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

        if (currentComState == 1)
        {
            return;

        }
        
        else if(currentComState == 2 && orderNum == 1)
        {
            if(howManyComTurn == 0 && howManyPlayerTurn == 0) //com�� ���϶� ó�� �����ϴ� ���̸�
            {
                if (comCardNum == 9 || comCardNum == 8) //computer�� ���̾Ƹ��10 �Ǵ� 9�� ���
                {
                    comBets = Random.Range(4, 8);
                }
                else if(comCardNum == 7 || comCardNum == 6)
                {
                    comBets = Random.Range(3, 7);
                }
                else if(comCardNum == 5 || comCardNum == 4)
                {
                    comBets = Random.Range(2, 4);
                }
                else if(comCardNum==3 || comCardNum == 2)
                {
                    comBets = Random.Range(1, 3);
                }
                else if (comCardNum == 0) //com�� ���̾Ƹ��1 �ΰ��(������ lose)
                {
                    //���� ������ ��
                    comBets = Random.Range(0, 3);
                 
                }
                comChips -= comBets; //������ ��ŭ ���� Ĩ���� ����
                Debug.Log("comact call");
                spObjects.ComActs();
            }

            else
            {
                //player�� betting������ ranRaise��ŭ �� �����ϵ��� ����
                int ranRaise = Random.Range(0, 3);
                if (comBets >= comChips || comBets + ranRaise >= comChips) //����?
                {
                    comBets = comChips;
                }
                else //��� ���
                {
                    comBets += ranRaise;

                    if(comCardNum == 0 || comCardNum == 1 || comCardNum == 2)
                    {
                        //���� die�ϴ� ���� com���� ����
                        int whenDie = Random.Range(1, 3); //������ 1���Ǵ�2�� �Դٰ��� ���� �� com�� die�� 
                        if (howManyComTurn == whenDie)
                        {
                            currentComState = 1; //die��
                        }
                    }
                }
                comChips -= comBets; //������ ��ŭ ���� Ĩ���� ����
                Debug.Log("comact call");
                spObjects.ComActs();
            }
        }
        
    }
}

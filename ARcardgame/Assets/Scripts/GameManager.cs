using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager; //�ٸ� script���� GameManager.manager�� �����ϸ� ����, �޼ҵ� ��� ���� 

    public int orderNum = 3; //0�̸� player ������ ����, 1�̸� com ������ ����
    public bool canCardDivide = false;

    public bool activate = false; //���� �ؽ�Ʈ ��� �� ���� �Ǵ� ���� ������ �����ϰ� �ϴ� ���� - S2ProcessText�� SpawnObjects ��� ����

    public int playerChips = 20;
    public int comChips = 20;

    public int comBets = 5;
    public int playerBets = 0;

    public GameState currentGameState = GameState.main;
    public int currentComState = 0; //�����Ϸ��� ���� 0, die�� �����ҽ� 1
    public int currentPlayerState = 0; //0�� ����Ʈ, die �����ҽ� 1, ���� acting ���� �� 2

    private Play2UI p2UI;

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
        if (activate)
        {
            //Debug.Log("activated");
        }
    }
    public void StartGame()
    {
        SetGameState(GameState.inGame); //�ҷ����� ������ "������" ���·� ����
    }

    void SetGameState(GameState state)
    {
        //���� ���¸� �����ϴ� �޼ҵ�, ���� ���� ��Ȳ �� �̰ɷ� state �����Ͻø� �˴ϴ�!
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
        //�ϴ� AI ������ �� �Ǿ������� UI ����� ���� �������� int�� �޾Ƽ� �������� ���������� �����ϰ� �߽��ϴ�.
        int ran = Random.Range(0, 2); //0�̸� ����, 1�̸� ����

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
            comBets = 5;//�ϴ� 5��
            comChips -= comBets; //������ ��ŭ ���� Ĩ���� ����
        }

    }
}

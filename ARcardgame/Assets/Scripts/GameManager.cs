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

    public int playerChips = 40;
    public int comChips = 40;

    public int comBets = 0;
    public int playerBets = 0;
    public int totalBets = 0;
    public int lastPlayerBets = 0; //������ ���� ���� Ȯ���� ���� ������ ������ ���� ����ϰ� �־����
    public int lastComBets = 0;
    public int playerAdds = 0;
    public int comAdds = 0;

    public GameState currentGameState = GameState.main;

    public int currentComState = 0; //0�� ����Ʈ, die�� �����ҽ� 1, ���� acting ���� �� 2
    public int currentPlayerState = 0; //0�� ����Ʈ, die �����ҽ� 1, ���� acting ���� �� 2

    private Play2UI p2UI;
    private SpawnObjects spObjects;
    private S2ProcessText processText;

    public string comCard;
    public int comCardNum = -1;
    public int playerCardNum;

    public int howManyComTurn = 0;
    public int howManyPlayerTurn = 0;
    public int totalTurns = 0;
    

    private int whenDie = -1; //comAI���� Die Ÿ�̹��� ��� ���� ����
    private int comsFlow = -1; //�̹� ������ �� AI�� ���� ���� ���ϱ� - ���� ���� : 0 , �߸� : 1, �ҽ��ϰ� : 2 
    private int checkCase = -1; //comAI������ ���� ����, case�� ������ �� ���� �������� ���Դϴ�.

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
        /*if (FindObjectOfType<GameProcessor>())
        {
            GameProcessor lastProcessor = FindObjectOfType<GameProcessor>();
            comChips = lastProcessor.saveComChips;
            playerChips = lastProcessor.savePlayerChips;

            Debug.Log(lastProcessor.saveComChips);
            Debug.Log(lastProcessor.savePlayerChips);

            Destroy(lastProcessor);
        }*/
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

    public void makeDefault()
    {
        orderNum = 3; 
        canCardDivide = false;

        activate = false; 

        comBets = 0;
        playerBets = 0;
        totalBets = 0;
        lastPlayerBets = 0; 
        lastComBets = 0;
        playerAdds = 0;
        comAdds = 0;

        currentGameState = GameState.main;

        currentComState = 0; 
        currentPlayerState = 0; 

        comCardNum = -1;

        howManyComTurn = 0;
        howManyPlayerTurn = 0;
        totalTurns = 0;


        whenDie = -1; 
        comsFlow = -1; 
        checkCase = -1;
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

    public GameState GetGameState()
    {
        return currentGameState;
    }

    public void GameOver()
    {
        activate = false;
        SetGameState(GameState.gameOver);       
    }

    public void FinalMotion()
    {
        spObjects.finalResult();
    }

    void RestartGame()
    {

    }

    void gameSetting()
    { 
        // �� ī�� ��Ƽ���� �ٲٱ�, �÷��̾� ī�� ������, UI ��ũ��Ʈ ������Ʈ �Ҵ�, ���� ���� ���� ���ϱ�
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
        else if (comCard.Equals("9ofDIamonds (Instance)"))
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

        processText = FindObjectOfType<Canvas>().GetComponent<S2ProcessText>();

        int flow = Random.Range(0, 5);
        if(flow == 3 || flow == 4) //�߸��� ���õ� Ȯ���� �� �� ����
        {
            flow = 1;
        }else if(flow == 2) //���� �ҽ��� ���� ������ �� �Ǿ 2�� �� �������� �������� ������
        {
            flow = 0;
        }
        comsFlow = flow;

        //�Ʒ��� �� �÷ο� �׽�Ʈ�� �ڵ��Դϴ�. �����ذ��� �׽�Ʈ ���� �ּ� ó��.
        //comsFlow = 0;
        //comsFlow = flow;
        Debug.Log("comsFlow: " + comsFlow);

        p2UI.UpdateComText(comChips);
        p2UI.UpdatePText(playerChips);

    }

    public void PlayerAct()
    {
        howManyPlayerTurn++;
        currentPlayerState = 2;
        p2UI.dieButton.interactable = true;
        //p2UI.okButton.interactable = true;
        
        
    }

    public void TurnEnds()
    {
        if (orderNum == 0 && currentPlayerState == 2) //player����, ����������
        {
            totalBets += playerBets;
            //lastPlayerBets = playerBets;
            playerAdds = playerBets - comAdds;
            playerChips -= playerBets;
            Debug.Log("player add " + playerAdds);

            if (wasItCall())
            {
                activate = false;
                p2UI.dieButton.interactable = false;
                p2UI.okButton.interactable = false;
                processText.SendMessage("Finish");
            }
            else
            {
                p2UI.UpdatePText(playerChips);
                currentPlayerState = 0;
                orderNum = 1;
                activate = false;
                playerBets = 0; //���� ���� �ʱ�ȭ
            }

        }
        else if (orderNum == 1 && currentComState == 2) //computer����, ����������
        {
            totalBets += comBets;
            //lastComBets = comBets;
            comAdds = comBets - playerAdds;
            Debug.Log("com add " + comAdds);

            if (wasItCall())
            {
                activate = false;
                p2UI.dieButton.interactable = false;
                p2UI.okButton.interactable = false;
                processText.SendMessage("Finish");
            }
            else
            {
                p2UI.UpdateComText(comChips); //null exception
                currentComState = 0;
                orderNum = 0;
                activate = false;
                comBets = 0;
            }
            
        }
    }

    bool wasItCall()
    {
        if(orderNum == 0)
        {
            if (playerAdds == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }else if(orderNum == 1)
        {
            if(comAdds == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
        
    }
 

    public void ComAI()
    {
        Debug.Log(howManyComTurn + "coms round");
        howManyComTurn++;
        Debug.Log(howManyComTurn + "coms round");
        Debug.Log("Die Timing set " + whenDie);
        currentComState = 2; //������ ��̰� �ݰ��ɱ�� ��ó������ Die�� ���� ���� �ּ�ȭ
        spObjects = GameObject.Find("Indicator").GetComponent<SpawnObjects>();
        if(comsFlow == 0) //�������� ����, ���� Die�� ���� ���� ����, ��븦 Die ��Ű�ڴٴ� ��������
        {
            if (howManyComTurn == 1 && howManyPlayerTurn == 0)
            {
                Debug.Log("why 0 - 1");
                if(comCardNum > 5)
                {
                    checkCase = 0;
                    if (playerCardNum < 3)
                    {
                        comBets = Random.Range(8, 13);
                        Debug.Log("here " + comBets);
                    }
                    else
                    {
                        comBets = Random.Range(6, 11);
                        Debug.Log("here " + comBets);
                    }
                }
                else if(comCardNum <=5 && comCardNum > 1 || comCardNum == 0)
                {
                    checkCase = 1;
                    if (playerCardNum < 3)
                    {
                        comBets = Random.Range(5, 11);
                        Debug.Log("here " + comBets);
                    }
                    else
                    {
                        comBets = Random.Range(3, 7);
                        Debug.Log("here " + comBets);
                    }
                }else if(comCardNum == 1)
                {
                    checkCase = 2;
                    //�ڽ��� ���ڰ� 2�� �� ������ ������ ����(��밡 �ڽ��� ace�ΰ� �䰡�ΰ��ϰ� ��)
                    comBets = Random.Range(9, 15);
                    Debug.Log("here " + comBets);
                }
            }
            else
            {
                if (checkCase == -1)
                {
                    if (comCardNum > 5)
                    {
                        checkCase = 0;
                    }
                    else if ((comCardNum <= 5 && comCardNum > 1) || comCardNum == 0)
                    {
                        checkCase = 1;
                    }
                    else if (comCardNum == 1)
                    {
                        checkCase = 2;
                    }
                }

                int ranRaise;

                switch (checkCase)
                {
                    case 0:
                        //���� ���ڰ� 5���� ŭ
                        if(playerCardNum < 3)
                        {
                            ranRaise = Random.Range(6, 11);
                        }
                        else
                        {
                            ranRaise = Random.Range(5, 9);
                        }
                        comBets = playerAdds + ranRaise;
                        Debug.Log("here " + comBets);
                        break;
                    case 1:
                        //���� ���ڰ� 5���� ���ų� �۰� ���⼭ 2�� ����
                        if(playerCardNum < 3)
                        {
                            ranRaise = Random.Range(5, 9);
                        }
                        else
                        {
                            ranRaise = Random.Range(4, 8);
                        }
                        comBets = playerAdds + ranRaise;
                        Debug.Log("here " + comBets);
                        break;
                    case 2:
                        //���� 2�� ����
                        ranRaise = Random.Range(7, 15);
                        comBets = playerAdds + ranRaise;
                        Debug.Log("here " + comBets);
                        break;
                }

            }

            if (currentComState == 1)
            {
                return;
            }
            if (comBets >= comChips)
            {
                comBets = comChips;
            }
            //���� ���� �� ���� �ڵ�
            Debug.Log("com will bet" + comBets);
            comChips -= comBets;
            Debug.Log("comact call");
            spObjects.ComActs();
        }
        else if(comsFlow == 1) //�߸�, �� ī���� ���ڿ� ���� �����ϰ� ���� ���� �����ϸ� player�� ī�� ���ڰ� ������ player�� �������� ���ϰ� �ణ�� ���Ӽ��� ��.
        {
            if (howManyComTurn == 1 && howManyPlayerTurn == 0) //com�� ���϶� ó�� �����ϴ� ���̸�
            {
                if (comCardNum == 9 || comCardNum == 8) //computer�� ���̾Ƹ��10 �Ǵ� 9�� ���
                {
                    checkCase = 0;
                    comBets = Random.Range(4, 8);
                    Debug.Log("here " + comBets);
                }
                else if (comCardNum == 7 || comCardNum == 6)
                {
                    checkCase = 1;
                    comBets = Random.Range(3, 7);
                    Debug.Log("here " + comBets);
                }
                else if (comCardNum == 5 || comCardNum == 4)
                {
                    checkCase = 2;
                    comBets = Random.Range(2, 4);
                    Debug.Log("here " + comBets);
                }
                else if (comCardNum == 3 || comCardNum == 2 || comCardNum == 1)
                {
                    checkCase = 3;
                    int dieOrNot = Random.Range(0, 5); //5���� 1�� Ȯ���� ���� �Ͽ��� Die ����
                    if(dieOrNot == 0)
                    {
                        whenDie = Random.Range(1, 3); //������ 1���Ǵ�2�� �Դٰ��� ���� �� com�� die��
                        whenDie += howManyComTurn;
                        comBets = Random.Range(1, 3);
                    }
                    else
                    {
                        comBets = Random.Range(1, 3);
                    }
                }
                else if (comCardNum == 0) //com�� ace�ΰ��(������ lose)
                {
                    checkCase = 4;
                    //���� ������ ��
                    int dieOrNot = Random.Range(0, 3); //3���� 1�� Ȯ���� ���� �Ͽ��� Die ����
                    if(dieOrNot == 0)
                    {
                        whenDie = Random.Range(1, 3); //������ 1���Ǵ�2�� �Դٰ��� ���� �� com�� die��
                        whenDie += howManyComTurn;
                        comBets = Random.Range(1, 3);
                        Debug.Log("here " + comBets);
                    }
                    else
                    {
                        comBets = Random.Range(1, 3);
                        Debug.Log("here " + comBets);
                    }
                }              
            }
            else //�÷��̾ ���̾��ų� ���� �� �� �̻� �� ���ư�, �÷��̾ ������ ������ ���� �����
            {
                if(checkCase == -1)
                {
                    if(comCardNum == 9 || comCardNum == 8)
                    {
                        checkCase = 0;
                    }else if(comCardNum == 7 || comCardNum == 6)
                    {
                        checkCase = 1;
                    }else if(comCardNum == 5 || comCardNum == 4)
                    {
                        checkCase = 2;
                    }else if(comCardNum == 3 || comCardNum == 2 || comCardNum == 1)
                    {
                        checkCase = 3;
                    }else if(comCardNum == 0)
                    {
                        checkCase = 4;
                    }
                }

                Debug.Log("CheckCase " + checkCase);

                if(howManyComTurn == whenDie)
                {
                    currentComState = 1;
                    return;
                }
                else
                {
                    //ranRaise�� �������� �� comBets�� ������ �÷��̾ ������ ���� ����.
                    int ranRaise = 0;
                    switch (checkCase)
                    {
                        case 0:
                            if(playerAdds >= 5)
                            {
                                //�÷��̾ �� ������ ������ �� ���, ������ ���� �߰�, ������ call�� �ƴ� ����� �ǵ���
                                ranRaise = Random.Range(1, 4);
                            }
                            else
                            {
                                ranRaise = Random.Range(1, 6);
                            }
                            comBets = playerAdds + ranRaise;
                            Debug.Log("here " + comBets);
                            break;
                        case 1:
                            if (playerAdds >= 6)
                            {
                                //�÷��̾ �� ������ ������ �� ���, ������ ���� �߰�, ������ call�� �ƴ� ����� �ǵ���
                                ranRaise = Random.Range(1, 3);
                            }
                            else
                            {
                                ranRaise = Random.Range(0, 5);
                            }
                            comBets = playerAdds + ranRaise;
                            Debug.Log("here " + comBets);
                            break;
                        case 2:
                            //com�� ī���� ���ڰ� 5~6�� �Ǹ� ����� Ȯ���� �� ������ �����Ƿ� ��Ȳ�� ���� ��븦 die ���Ѻ����� �ϰų� ������ die�� ������
                            if(playerAdds >= 6)
                            {
                                //�Ƹ� ��밡 com�� die ��Ű���� �ϴ� �ǵ�
                                if (playerCardNum < 5) //��Ƽ�� com�� �̱�
                                {
                                    ranRaise = Random.Range(0, 4);
                                }else if (playerCardNum >= 5) //com�� �� Ȯ�� ����
                                {
                                    if (comChips < 10 || comChips < playerAdds) //���� Ĩ�� ������ 9�� �̸�
                                    {
                                        int ran = Random.Range(0, 2); //�������� ���� ����
                                        if(ran == 0)
                                        {
                                            currentComState = 1; //�ٷ� Die ����
                                            return;
                                        }
                                        else
                                        {
                                            ranRaise = Random.Range(0, 3);
                                        }
                                    }
                                    else
                                    {
                                        ranRaise = Random.Range(0, 3);
                                    }
                                }
                            }else if (playerAdds >= 3 && playerAdds < 6)
                            {
                                ranRaise = Random.Range(0, 3);
                            }
                            else
                            {
                                if(playerCardNum < 5)
                                {
                                    ranRaise = Random.Range(1, 3);
                                }else if(playerCardNum >= 5)
                                {
                                    ranRaise = Random.Range(0, 3);
                                }
                            }
                            comBets = playerAdds + ranRaise;
                            Debug.Log("here " + comBets);
                            break;
                        case 3:
                            //���� �̱� Ȯ���� ���� ����, ����� ���ÿ� ���� �ٷ� Die�� �����ϱ⵵ ��, �ϴ� ���ɽ����� ���� but �������� �� ���� �ɱ⵵ ��
                            if(whenDie == -1 && howManyComTurn == 1) //���� ���� �ƴϾ Die üũ�� �� �Ǿ��ٸ� ù ��°�� ���� Die üũ �õ�
                            {
                                int dieOrNot = Random.Range(0, 5); //5���� 1�� Ȯ���� ���� �Ͽ��� Die ����
                                if (dieOrNot == 0)
                                {
                                    whenDie = Random.Range(1, 3);
                                    whenDie += howManyComTurn;
                                }
                                else
                                {
                                    whenDie = -1; //�״�� �α�
                                }
                            }

                            if(playerAdds >= 6) //�÷��̾��� ������ ����
                            {
                                if(comChips <= playerAdds)
                                {
                                    currentComState = 1;
                                    return;
                                }
                                else
                                {
                                    int ran = Random.Range(0, 3);
                                    if(ran == 0)
                                    {
                                        currentComState = 1;
                                        return;
                                    }
                                    else
                                    {
                                        ranRaise = Random.Range(0, 2);
                                    }
                                }
                            }else if(playerAdds < 6 && playerAdds >= 3){
                                if (comChips <= 10) {
                                    if (playerCardNum >= 7)
                                    {
                                        currentComState = 1;
                                        return;
                                    }
                                    else
                                    {
                                        int ran = Random.Range(0, 2);
                                        if (ran == 0)
                                        {
                                            currentComState = 1;
                                            return;
                                        }
                                        else
                                        {
                                            ranRaise = 100; //�����ϰ� ����� ���Դϴ�.
                                        }
                                    }
                                }
                                else
                                {
                                    if(playerCardNum < 3)
                                    {
                                        ranRaise = Random.Range(1, 4);
                                    }
                                    else
                                    {
                                        ranRaise = Random.Range(0, 3);
                                    }
                                }
                            }
                            else //�÷��̾��� �ҽ��� ����
                            {
                                //com�� ������ �θ��⵵ ��
                                int ran = Random.Range(0, 3);
                                if(ran == 0)
                                {
                                    ranRaise = Random.Range(5, 10);
                                }
                                else
                                {
                                    if(playerCardNum > 7)
                                    {
                                        ranRaise = Random.Range(0, 3);
                                    }
                                    else
                                    {
                                        ranRaise = Random.Range(0, 5);
                                    }
                                }
                            }

                            comBets = playerAdds + ranRaise;
                            Debug.Log("here " + comBets);
                            break;
                        case 4:
                            //���� �̱� Ȯ���� ���� ����, ���߿� ���̸� ������ Ȯ�� ����, ������ player�� ������ ���ϰ� �Ѵٸ� ������ Ȯ���� �� ����
                            if (whenDie == -1 && howManyComTurn == 1)
                            {
                                int dieOrNot = Random.Range(0, 3); //3���� 1�� Ȯ���� ���� �Ͽ��� Die ����
                                if (dieOrNot == 0)
                                {
                                    whenDie = Random.Range(1, 3);
                                    whenDie += howManyComTurn;
                                    Debug.Log("Die activated: " + whenDie);
                                }
                                else
                                {
                                    whenDie = -1;
                                }
                            }

                            if(playerAdds < 2)
                            {
                                if(playerCardNum > 7)
                                {
                                    ranRaise = Random.Range(0, 3);
                                }
                                else
                                {
                                    ranRaise = Random.Range(1, 5);
                                }
                            }else if(playerAdds >= 2 && playerAdds < 5)
                            {
                                if (playerCardNum > 7)
                                {
                                    ranRaise = Random.Range(0, 2);
                                }
                                else
                                {
                                    ranRaise = Random.Range(1, 4);
                                }
                            }
                            else
                            {
                                if(playerCardNum > 7)
                                {
                                    if(comChips <= playerAdds)
                                    {
                                        currentComState = 1;
                                        return;
                                    }
                                    else
                                    {
                                        int ran = Random.Range(0, 2);
                                        if (ran == 0)
                                        {
                                            currentComState = 1;
                                            return;
                                        }
                                        else
                                        {
                                            ranRaise = Random.Range(0, 2);
                                        }
                                    }                                   
                                }
                                else
                                {
                                    if(comChips <= playerAdds)
                                    {
                                        int ran = Random.Range(0, 2);
                                        if(ran == 0)
                                        {
                                            currentComState = 1;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        int ran = Random.Range(0, 4);
                                        if (ran == 0)
                                        {
                                            currentComState = 1;
                                            return;
                                        }
                                        else
                                        {
                                            ranRaise = Random.Range(0, 4);
                                        }
                                    }
                                }
                            }
                            comBets = playerAdds + ranRaise;
                            Debug.Log("here " + comBets);
                            break;
                    }
                    comBets = playerAdds + ranRaise;
                }
            }

            if(currentComState == 1)
            {
                return;
            }
            if(comBets >= comChips)
            {
                comBets = comChips;
            }
            Debug.Log("com will bet" + comBets);
            comChips -= comBets; //������ ��ŭ ���� Ĩ���� ����
            Debug.Log("comact call");
            spObjects.ComActs();
        }
        /*else if(comsFlow == 2) //�ҽ��� ����, �÷��̾��� ī�� ���ڿ� ���� ���� ����, �÷��̾ �ڽ��� ���ڰ� ���ٰ� �����ϰ� ���� ���� ����, Die�� ������ Ȯ���� ����
        {

        }*/


            

    }
}

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

    public int playerChips = 40;
    public int comChips = 40;

    public int comBets = 0;
    public int playerBets = 0;
    public int totalBets = 0;
    public int lastPlayerBets = 0; //게임의 종료 여부 확인을 위해 직전에 베팅한 수를 기억하고 있어야함
    public int lastComBets = 0;
    public int playerAdds = 0;
    public int comAdds = 0;

    public GameState currentGameState = GameState.main;

    public int currentComState = 0; //0이 디폴트, die를 선택할시 1, 현재 acting 중일 시 2
    public int currentPlayerState = 0; //0이 디폴트, die 선택할시 1, 현재 acting 중일 시 2

    private Play2UI p2UI;
    private SpawnObjects spObjects;
    private S2ProcessText processText;

    public string comCard;
    public int comCardNum = -1;
    public int playerCardNum;

    public int howManyComTurn = 0;
    public int howManyPlayerTurn = 0;
    public int totalTurns = 0;
    

    private int whenDie = -1; //comAI에서 Die 타이밍을 잡기 위한 변수
    private int comsFlow = -1; //이번 라운드의 컴 AI의 베팅 경향 정하기 - 세게 나감 : 0 , 중립 : 1, 소심하게 : 2 
    private int checkCase = -1; //comAI에서만 쓰는 변수, case를 나누기 더 쉽게 만들어놓은 것입니다.

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
        SetGameState(GameState.inGame); //불러오는 시점에 "게임중" 상태로 만듦

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
        //게임 상태를 결정하는 메소드, 게임 오버 상황 시 이걸로 state 변경하시면 됩니다!
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
        // 컴 카드 인티저로 바꾸기, 플레이어 카드 나누기, UI 스크립트 오브젝트 할당, 컴의 베팅 경향 정하기
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
        if(flow == 3 || flow == 4) //중립이 선택될 확률이 좀 더 높게
        {
            flow = 1;
        }else if(flow == 2) //아직 소심한 베팅 구현이 덜 되어서 2일 때 공격적인 베팅으로 가도록
        {
            flow = 0;
        }
        comsFlow = flow;

        //아래는 각 플로우 테스트용 코드입니다. 수정해가며 테스트 이후 주석 처리.
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
        if (orderNum == 0 && currentPlayerState == 2) //player차례, 게임진행중
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
                playerBets = 0; //각종 값들 초기화
            }

        }
        else if (orderNum == 1 && currentComState == 2) //computer차례, 게임진행중
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
        currentComState = 2; //게임의 재미가 반감될까봐 맨처음부터 Die를 고르는 경우는 최소화
        spObjects = GameObject.Find("Indicator").GetComponent<SpawnObjects>();
        if(comsFlow == 0) //공격적인 베팅, 먼저 Die는 절대 하지 않음, 상대를 Die 시키겠다는 느낌으로
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
                    //자신의 숫자가 2일 때 오히려 과감한 베팅(상대가 자신이 ace인가 긴가민가하게 함)
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
                        //컴의 숫자가 5보다 큼
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
                        //컴의 숫자가 5보다 같거나 작고 여기서 2는 제외
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
                        //컴이 2를 가짐
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
            //베팅 결정 후 공통 코드
            Debug.Log("com will bet" + comBets);
            comChips -= comBets;
            Debug.Log("comact call");
            spObjects.ComActs();
        }
        else if(comsFlow == 1) //중립, 컴 카드의 숫자에 따라 세세하게 베팅 수를 조절하며 player의 카드 숫자가 작을시 player가 다이하지 못하게 약간의 속임수를 씀.
        {
            if (howManyComTurn == 1 && howManyPlayerTurn == 0) //com이 선일때 처음 베팅하는 것이면
            {
                if (comCardNum == 9 || comCardNum == 8) //computer가 다이아몬드10 또는 9인 경우
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
                    int dieOrNot = Random.Range(0, 5); //5분의 1의 확률로 나중 턴에서 Die 선택
                    if(dieOrNot == 0)
                    {
                        whenDie = Random.Range(1, 3); //베팅이 1번또는2번 왔다갔다 했을 때 com이 die함
                        whenDie += howManyComTurn;
                        comBets = Random.Range(1, 3);
                    }
                    else
                    {
                        comBets = Random.Range(1, 3);
                    }
                }
                else if (comCardNum == 0) //com이 ace인경우(무조건 lose)
                {
                    checkCase = 4;
                    //베팅 조금할 것
                    int dieOrNot = Random.Range(0, 3); //3분의 1의 확률로 나중 턴에서 Die 선택
                    if(dieOrNot == 0)
                    {
                        whenDie = Random.Range(1, 3); //베팅이 1번또는2번 왔다갔다 했을 때 com이 die함
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
            else //플레이어가 선이었거나 턴이 한 번 이상씩 다 돌아감, 플레이어가 직전에 베팅한 수도 고려함
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
                    //ranRaise가 더해지기 전 comBets는 직전의 플레이어가 베팅한 수와 같다.
                    int ranRaise = 0;
                    switch (checkCase)
                    {
                        case 0:
                            if(playerAdds >= 5)
                            {
                                //플레이어가 꽤 과감한 베팅을 한 경우, 오히려 적게 추가, 하지만 call이 아닌 레이즈가 되도록
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
                                //플레이어가 꽤 과감한 베팅을 한 경우, 오히려 적게 추가, 하지만 call이 아닌 레이즈가 되도록
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
                            //com의 카드의 숫자가 5~6쯤 되면 우승의 확률이 반 정도가 됐으므로 상황에 따라 상대를 die 시켜보고자 하거나 본인이 die를 선택함
                            if(playerAdds >= 6)
                            {
                                //아마 상대가 com을 die 시키고자 하는 의도
                                if (playerCardNum < 5) //버티면 com이 이김
                                {
                                    ranRaise = Random.Range(0, 4);
                                }else if (playerCardNum >= 5) //com이 질 확률 높음
                                {
                                    if (comChips < 10 || comChips < playerAdds) //남은 칩의 개수가 9개 미만
                                    {
                                        int ran = Random.Range(0, 2); //다이할지 말지 결정
                                        if(ran == 0)
                                        {
                                            currentComState = 1; //바로 Die 선택
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
                            //컴이 이길 확률이 낮은 상태, 상대의 베팅에 따라서 바로 Die를 선택하기도 함, 일단 조심스럽게 베팅 but 올인으로 한 수를 걸기도 함
                            if(whenDie == -1 && howManyComTurn == 1) //컴의 선이 아니어서 Die 체크가 안 되었다면 첫 번째에 한해 Die 체크 시도
                            {
                                int dieOrNot = Random.Range(0, 5); //5분의 1의 확률로 나중 턴에서 Die 선택
                                if (dieOrNot == 0)
                                {
                                    whenDie = Random.Range(1, 3);
                                    whenDie += howManyComTurn;
                                }
                                else
                                {
                                    whenDie = -1; //그대로 두기
                                }
                            }

                            if(playerAdds >= 6) //플레이어의 과감한 베팅
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
                                            ranRaise = 100; //올인하게 만드는 것입니다.
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
                            else //플레이어의 소심한 베팅
                            {
                                //com의 착각을 부르기도 함
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
                            //컴이 이길 확률이 없는 상태, 도중에 다이를 선택할 확률 높음, 하지만 player가 베팅을 약하게 한다면 따라가줄 확률을 더 높게
                            if (whenDie == -1 && howManyComTurn == 1)
                            {
                                int dieOrNot = Random.Range(0, 3); //3분의 1의 확률로 나중 턴에서 Die 선택
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
            comChips -= comBets; //베팅한 만큼 가진 칩에서 제거
            Debug.Log("comact call");
            spObjects.ComActs();
        }
        /*else if(comsFlow == 2) //소심한 베팅, 플레이어의 카드 숫자에 가장 많이 의존, 플레이어가 자신의 숫자가 높다고 착각하게 만들 수도 있음, Die를 선언할 확률이 높음
        {

        }*/


            

    }
}

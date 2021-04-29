using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class S2ProcessText : MonoBehaviour
{
    //private int orderNum;
    //private int playerBet = 0;
    private int comBet = 0;

    public TextMeshProUGUI processText;
    public Play2UI p2UI;
    public float time = 0;

    public SpawnObjects sp;

    private bool startBet = false;

    void Start()
    {
        //orderNum = GameManager.manager.orderNum; //���� ���ÿ��� ���� ���� �ҷ����� ( 0�̸� �÷��̾� ����, 1�̸� Com�� ���� )
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.setBoard == 0 && !startBet) //������ ���� �ȵ� ���
        {
            processText.SetText("Tap the bottom to make your game board!");
            //Debug.Log("order well?" + GameManager.manager.orderNum);
        }
        else if (sp.setBoard == 1 && !startBet) //������ ������ ���
        {
            switch (GameManager.manager.orderNum)
            {
                case 0:
                    //time += Time.deltaTime;
                    startBet = true;
                    ForPlayerText();
                    
                    break;
                case 1:
                    //time += Time.deltaTime;
                    startBet = true;
                    ForComText();
                    
                    break;
                default:
                    break;
            }
        }
        else if (startBet)
        {
            time += Time.deltaTime;
        }
        //time += Time.deltaTime;

    }

    public void ForPlayerText()
    {

        if (!GameManager.manager.activate && GameManager.manager.currentPlayerState == 0)
        {
            processText.SetText("It's your turn. Please choose your action.");
            GameManager.manager.activate = true;
            GameManager.manager.PlayerAct();
        }
        
        //Debug.Log(GameManager.manager.activate);
        //Debug.Log(GameManager.manager.currentPlayerState);

        /*if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("get touch");
            if(!GameManager.manager.activate && GameManager.manager.currentPlayerState == 0)
            {
                processText.SetText("Please choose your action.");
                GameManager.manager.activate = true;
                GameManager.manager.PlayerAct();
            }

        }*/

            //player�� �Ͽ� Ȱ��ȭ
         /*if (num < 3)
        {
            processText.SetText("It's your turn.");
        }

        else if(num > 3)
        {
            processText.SetText("Please choose your action.");
            GameManager.manager.activate = true;
            GameManager.manager.PlayerAct();
        }*/

        //else if(processText.Equals("Please choose your action."))
        //{
        //    GameManager.manager.activate = true;
        //    GameManager.manager.PlayerAct();
        //    //playerBet = GameManager.manager.playerBets;
        //} 

        //player�� die button ����

        //player�� ���� �Ϸ�
        /*if (GameManager.manager.currentPlayerState == 2 && !GameManager.manager.activate)
        {
            processText.SetText("You bet on " + GameManager.manager.playerBets + " chips.");
            GameManager.manager.currentPlayerState = 0;
            GameManager.manager.orderNum = 1;
            Invoke("ForComText", 3);
        }*/
        
    }

    public void ForComText()
    {
        if (!GameManager.manager.activate && GameManager.manager.currentComState == 0)
        {
            processText.SetText("It's Com's turn. Com is thinking about what to do.");
            GameManager.manager.activate = true;
            GameManager.manager.ComAI();
        }
            

        /*if (Input.touchCount > 0)
        {
            Debug.Log("where is my touch...");
            if (!GameManager.manager.activate)
            {
                Debug.Log("where is my touch...");
                processText.SetText("Com is thinking about what to do.");
                GameManager.manager.activate = true;
                GameManager.manager.ComAI();
            }
        }*/

        if (GameManager.manager.currentComState == 1)
        {
            Invoke("WhoDied", 3);
        }

        /*if (GameManager.manager.currentComState == 2 && !GameManager.manager.activate)
        {
            Invoke("NextTurn", 3);
            
        }*/

        //time = 0;
        //com�� �Ͽ� Ȱ��ȭ
        //int num = (int)time;

        /*if(num < 3)
        {
            processText.SetText("It's Com's turn.");
        }

        else if(num > 3)
        {
            processText.SetText("Com is thinking about what to do.");
            GameManager.manager.activate = true;
            GameManager.manager.ComAI();
            //comBet = GameManager.manager.comBets;
        }

        //else if(processText.Equals("Com is thinking about what to do."))
        //{
        //    GameManager.manager.activate = true;
        //    GameManager.manager.ComAI();
        //    comBet = GameManager.manager.comBets;
        //}

        else if(GameManager.manager.currentComState == 1 && num > 10)
        {
            processText.SetText("Com gave up the game.");
            GameManager.manager.GameOver();
        }*/

        /*else if(GameManager.manager.currentComState == 0 && !GameManager.manager.activate)
        {
            Debug.Log("activate is " + GameManager.manager.activate);
            processText.SetText("Com bet on " + GameManager.manager.comBets + " chips.");
            GameManager.manager.orderNum = 0;
            Invoke("ForPlayerText", 3);
        }*/

    }

    public void WhoDied()
    {
        //die�� ������ ���� ã�Ƴ� text ��ȯ
        if(GameManager.manager.currentComState == 1)
        {
            processText.SetText("Com gave up this game!");
            GameManager.manager.GameOver();
        }
        else if(GameManager.manager.currentPlayerState == 1)
        {
            processText.SetText("You gave up this game!");
            GameManager.manager.GameOver();
        }
    }

    public void NextTurn()
    {
        if (GameManager.manager.orderNum == 0 && GameManager.manager.currentPlayerState == 2)
        {
            processText.SetText("You bet on " + GameManager.manager.playerBets + " chips.");
            GameManager.manager.playerChips -= GameManager.manager.playerBets;
            p2UI.UpdatePText(GameManager.manager.playerChips);
            GameManager.manager.currentPlayerState = 0;
            GameManager.manager.orderNum = 1;
            GameManager.manager.activate = false;
            GameManager.manager.playerBets = 0; //���� ���� �ʱ�ȭ
            Invoke("ForComText", 3);
        }
        else if (GameManager.manager.orderNum == 1 && GameManager.manager.currentComState == 2)
        {
            processText.SetText("Com bet on " + GameManager.manager.comBets + " chips.");
            GameManager.manager.comChips -= GameManager.manager.comBets;
            p2UI.UpdateComText(GameManager.manager.comChips);
            GameManager.manager.currentComState = 0;
            GameManager.manager.orderNum = 0;
            GameManager.manager.activate = false;
            Invoke("ForPlayerText", 3);
        }
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(3.0f);
    }
}

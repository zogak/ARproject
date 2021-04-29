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
    public float time = 0;

    public SpawnObjects sp;

    void Start()
    {
        //orderNum = GameManager.manager.orderNum; //���� ���ÿ��� ���� ���� �ҷ����� ( 0�̸� �÷��̾� ����, 1�̸� Com�� ���� )
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.setBoard == 0) //������ ���� �ȵ� ���
        {
            processText.SetText("Tap the bottom to make your game board!");
            //Debug.Log("order well?" + GameManager.manager.orderNum);
        }
        else if (sp.setBoard == 1) //������ ������ ���
        {
            switch (GameManager.manager.orderNum)
            {
                case 0:
                    time += Time.deltaTime;
                    ForPlayerText();
                    break;
                case 1:
                    time += Time.deltaTime;
                    ForComText();
                    break;
                default:
                    break;
            }
        }
        //time += Time.deltaTime;

    }

    public void ForPlayerText()
    {

        int num = (int)time;

        //player�� �Ͽ� Ȱ��ȭ
        if(num < 3)
        {
            processText.SetText("It's your turn.");
        }

        else if(num > 3)
        {
            processText.SetText("Please choose your action.");
            GameManager.manager.activate = true;
            GameManager.manager.PlayerAct();
        }

        //else if(processText.Equals("Please choose your action."))
        //{
        //    GameManager.manager.activate = true;
        //    GameManager.manager.PlayerAct();
        //    //playerBet = GameManager.manager.playerBets;
        //} 

        //player�� die button ����
        else if(GameManager.manager.currentPlayerState == 1)
        {
            processText.SetText("You gave up the game.");
            GameManager.manager.GameOver();
        }

        //player�� ���� �Ϸ�
        else if (GameManager.manager.currentPlayerState == 2 && !GameManager.manager.activate)
        {
            processText.SetText("You bet on " + GameManager.manager.playerBets + " chips.");
            StartCoroutine(WaitForSecond());
            time = 0;
            GameManager.manager.currentPlayerState = 0;
            GameManager.manager.orderNum = 1;
        }
        
    }

    public void ForComText()
    {
        //com�� �Ͽ� Ȱ��ȭ
        int num = (int)time;

        if(num < 3)
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
        }
        
        else if(GameManager.manager.currentComState == 0 && !GameManager.manager.activate)
        {
            Debug.Log("activate is " + GameManager.manager.activate);
            processText.SetText("Com bet on " + GameManager.manager.comBets + " chips.");
            StartCoroutine(WaitForSecond());
            time = 0;
            GameManager.manager.orderNum = 0;
        }
        
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(3.0f);
    }
}

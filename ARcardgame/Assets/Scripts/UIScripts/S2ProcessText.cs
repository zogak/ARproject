using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class S2ProcessText : MonoBehaviour
{
    private int orderNum;
    private int playerBet = 0;
    private int comBet = 0;

    public TextMeshProUGUI processText;
    public float time = 0;

    public SpawnObjects sp;

    void Start()
    {
        orderNum = GameManager.manager.orderNum; //���� ���ÿ��� ���� ���� �ҷ����� ( 0�̸� �÷��̾� ����, 1�̸� Com�� ���� )
 
    }

    // Update is called once per frame
    void Update()
    {
        if(sp.setBoard == 0)
        {
            processText.SetText("Tab the bottom to make your game board!");
        }
        else if(sp.setBoard == 1)
        {
            switch (orderNum)
            {
                case 0:
                    time = 0;
                    ForPlayerText();
                    break;
                case 1:
                    time = 0;
                    ForComText();
                    break;
                default:
                    break;
            }
        }
        time += Time.deltaTime;
        
    }

    private void ForPlayerText()
    {

        int num = (int)time;

        //player�� �Ͽ� Ȱ��ȭ
        if(num < 3)
        {
            processText.SetText("It's your turn.");
        }

        if(num > 3)
        {
            processText.SetText("Please choose your action.");
        }

        if(processText.Equals("Please choose your action."))
        {
            GameManager.manager.activate = true;
            GameManager.manager.PlayerAct();
            playerBet = GameManager.manager.playerBets;
        } 

        //player�� die button ����
        if(GameManager.manager.currentPlayerState == 1)
        {
            processText.SetText("You gave up the game.");
            GameManager.manager.GameOver();
        }

        //player�� ���� �Ϸ�
        if (!GameManager.manager.activate)
        {
            processText.SetText("You bet on " + playerBet + " chips.");
        }
        
    }

    private void ForComText()
    {
        //com�� �Ͽ� Ȱ��ȭ

        int num = (int)time;

        if(num < 3)
        {
            processText.SetText("It's Com's turn.");
        }

        if(num > 3)
        {
            processText.SetText("Com is thinking about what to do.");
        }
        
        if(processText.Equals("Com is thinking about what to do."))
        {
            GameManager.manager.activate = true;
            GameManager.manager.ComAI();
            comBet = GameManager.manager.comBets;
        }

        if(GameManager.manager.currentComState == 1 && num > 6)
        {
            processText.SetText("Com gave up the game.");
            GameManager.manager.GameOver();
        }
        
        if(GameManager.manager.currentComState == 0 && !GameManager.manager.activate)
        {
            processText.SetText("Com bet on " + comBet + " chips.");
        }
        
    }
}

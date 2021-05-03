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

    //public SpawnObjects sp;

    void Start()
    {
        processText.SetText("Tap the bottom to make your game board!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ForPlayerText()
    {
        processText.SetText("It's your turn. Please choose your action.");
        GameManager.manager.activate = true;
        GameManager.manager.PlayerAct();

    }

    public void ForComText()
    {
        processText.SetText("It's Com's turn. Com is thinking about what to do.");
        GameManager.manager.activate = true;
        GameManager.manager.ComAI();
        Debug.Log("ComAI called");


        if (GameManager.manager.currentComState == 1)
        {
            Invoke("WhoDied", 3);
        }

    }

    public void WhoDied()
    {
        //die를 선언한 쪽을 찾아내 text 전환
        if(GameManager.manager.currentComState == 1)
        {
            processText.SetText("Com gave up this game!");
            Invoke("Finish", 3);
        }
        else if(GameManager.manager.currentPlayerState == 1)
        {
            processText.SetText("You gave up this game!");
            Invoke("Finish", 3);
        }
    }

    public void NextTurn() //OK 버튼과 SpawnObject의 함수가 호출
    {
        if (GameManager.manager.orderNum == 0 && GameManager.manager.currentPlayerState == 2)
        {
            processText.SetText("You bet on " + GameManager.manager.playerBets + " chips.");
            Invoke("invokeSomeTime", 3);
        }
        else if (GameManager.manager.orderNum == 1 && GameManager.manager.currentComState == 2)
        {
            processText.SetText("Com bet on " + GameManager.manager.comBets + " chips.");
            Invoke("invokeSomeTime", 3);
        }
    }

    public void Finish()
    {
        
        if(GameManager.manager.orderNum == 0)
        {
            GameManager.manager.playerChips -= GameManager.manager.playerBets;
            p2UI.UpdatePText(GameManager.manager.playerChips);
        }
        else if(GameManager.manager.orderNum == 1)
        {
            p2UI.UpdateComText(GameManager.manager.comChips);
        }
        

        processText.SetText("Game is finish!");
        GameManager.manager.GameOver();
    }


    void invokeSomeTime()
    {
        GameManager.manager.TurnEnds();
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(3.0f);
    }
}

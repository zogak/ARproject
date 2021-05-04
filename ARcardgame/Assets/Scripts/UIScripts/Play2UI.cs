using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Play2UI : MonoBehaviour
{
    [SerializeField]
    private Sprite[] imageSprites;

    public Button dieButton;
    public Button okButton;
    public Button continueB;
    public Button backB;

    public TextMeshProUGUI comChips;
    public TextMeshProUGUI playerChips;

    public Image comCard;

    public GameObject resultPanel;
    public TextMeshProUGUI whoWin;
    public TextMeshProUGUI chipResult;

    public S2ProcessText process;

    public void DieButton()
    {
        GameManager.manager.currentPlayerState = 1;
        GameManager.manager.activate = false;
        okButton.interactable = false;
        dieButton.interactable = false;
        process.SendMessage("WhoDied");
    }

    public void OKButton()
    {
        if(GameManager.manager.activate == true)
        {
            GameManager.manager.activate = false;
            okButton.interactable = false;
            dieButton.interactable = false;
            process.SendMessage("NextTurn");
        }
        else
        {
            okButton.interactable = false;
            dieButton.interactable = false;
            process.SendMessage("Finish");
        }
    }

    public void UpdateComText(int update)
    {
        comChips.SetText("X " + update);
    }

    public void UpdatePText(int update)
    {
        playerChips.SetText("X " + update);
    }

    public void ComCardImageUpdate(int cardNum)
    {
        comCard.sprite = imageSprites[cardNum];
    }

    public void continueButton()
    {
        //chip 오브젝트 전부 제거, 배경음 그대로, 보드 그대로, comchips, playerchips 제외한 manager변수들 초기화
        /*GameObject[] chips = GameObject.FindGameObjectsWithTag("Chip");
        for(int i = 0; i < chips.Length; i++)
        {
            Destroy(chips[i]);
        }
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        for()*/
        GameManager.manager.makeDefault();
        FindObjectOfType<RoundSavor>().saveComChips = GameManager.manager.comChips;
        FindObjectOfType<RoundSavor>().savePlayerChips = GameManager.manager.playerChips;

        /*GameProcessor processor = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameProcessor>();
        processor.saveComChips = GameManager.manager.comChips;
        processor.savePlayerChips = GameManager.manager.playerChips;*/
        SceneManager.LoadScene("GamePlay");
    }

    public void backButton()
    {
        //모두 default 상태로 바꾼 뒤 mainscene으로 돌아감
        SceneManager.LoadScene("MainScene");
    }

    public void ResultUI()
    {
        string win = " ";
        resultPanel.SetActive(true);
        if(GameManager.manager.currentComState == 1)
        {
            whoWin.SetText("You Win!");
            win = "Player";
            GameManager.manager.playerChips += GameManager.manager.totalBets;
            UpdatePText(GameManager.manager.playerChips);
        }
        else if(GameManager.manager.currentPlayerState == 1)
        {
            whoWin.SetText("Com Wins!");
            win = "Com";
            GameManager.manager.comChips += GameManager.manager.totalBets;
            UpdateComText(GameManager.manager.comChips);
        }
        else
        {
            if (GameManager.manager.comCardNum > GameManager.manager.playerCardNum)
            {
                //whoWin.GetComponent<TextMeshPro>().renderer.material.color = Color.red;
                whoWin.SetText("Com Wins!");
                win = "Com";
                GameManager.manager.comChips += GameManager.manager.totalBets;
                UpdateComText(GameManager.manager.comChips);
            }
            else
            {
                whoWin.SetText("You Win!");
                win = "Player";
                GameManager.manager.playerChips += GameManager.manager.totalBets;
                UpdatePText(GameManager.manager.playerChips);
            }
        }
        chipResult.SetText(win + " got " + GameManager.manager.totalBets + " chips\n" + "Chips left for you: " + GameManager.manager.playerChips + "\nChips left for Com: " + GameManager.manager.comChips);


        if(GameManager.manager.playerChips == 0 || GameManager.manager.comChips == 0)
        {
            continueB.interactable = false;
        }
    }
    
}

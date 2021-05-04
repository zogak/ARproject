using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Play2UI : MonoBehaviour
{
    [SerializeField]
    private Sprite[] imageSprites;

    public Button dieButton;
    public Button okButton;
    public Button continueB;
    public Button quitB;

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

    }

    public void quitButton()
    {

    }

    public void ResultUI()
    {
        string win = " ";
        resultPanel.SetActive(true);
        if (GameManager.manager.comCardNum > GameManager.manager.playerCardNum)
        {
            //whoWin.GetComponent<TextMeshPro>().renderer.material.color = Color.red;
            whoWin.SetText("Com Wins!");
            win = "Com";
        }
        else
        {
            whoWin.SetText("You Win!");
            win = "Player";
        }

        chipResult.SetText(win + " got" + GameManager.manager.totalBets + " chips\n" + "Chips left for you: " + GameManager.manager.playerChips + "\nChips left for Com: " + GameManager.manager.comChips);

        if(GameManager.manager.playerChips == 0 || GameManager.manager.comChips == 0)
        {
            continueB.interactable = false;
        }
    }
    
}

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

    public TextMeshProUGUI comChips;
    public TextMeshProUGUI playerChips;

    public Image comCard;

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
}

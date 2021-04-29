using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Play2UI : MonoBehaviour
{
    public Button dieButton;
    public Button okButton;

    public TextMeshProUGUI comChips;
    public TextMeshProUGUI playerChips;

    public Image comCard;

    public void DieButton()
    {
        GameManager.manager.currentPlayerState = 1;
    }

    public void OKButton()
    {
        GameManager.manager.activate = false;
        Debug.Log("activate is " + GameManager.manager.activate);
    }

    public void UpdateComText()
    {

    }

    public void UpdatePText()
    {

    }
}

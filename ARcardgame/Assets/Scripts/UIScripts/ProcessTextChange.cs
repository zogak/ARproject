using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ProcessTextChange : MonoBehaviour
{
    public TextMeshProUGUI processText;
    public Button Next;

    private bool orderEnd = false;

    public float time = 0;
    int num;

    private bool allReady = false;
    //public bool cardMade = false;

    void Start()
    {
        processText.SetText("Welcome to the world of fun AR card games!");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        num = (int)time;

        if (!GameManager.manager.activate)
        {
            switch (num)
            {
                //4초 후 텍스트 변환
                case 4:
                    {
                        processText.SetText("Before dividing the cards, let's decide the betting order.");
                        break;
                    }
                //8초 후 텍스트 변환
                case 8:
                    {
                        processText.SetText("Touch the arrow!");
                        GameManager.manager.activate = true;
                        break;
                    }
            }
        }
        
        //순서가 정해지고 나면 카드를 배분한다는 텍스트로 변환
        if (!(GameManager.manager.orderNum== -1) && !orderEnd)
        {
            if(GameManager.manager.orderNum == 0)
            {
                processText.SetText("You will bet first!");
                orderEnd = true;
                Invoke("DividingText", 2);
            }
            else if(GameManager.manager.orderNum == 1)
            {
                processText.SetText("Com will bet first!");
                orderEnd = true;
                Invoke("DividingText", 2);
            }
        }

        if (allReady)
        {
            Next.gameObject.SetActive(true);
        }

    }

    //카드가 배분되고 나면 다음 단계를 위해 버튼을 눌러달라는 텍스트로 변환
    private void DividingText()
    {
        processText.SetText("Cards are being distributed...");
        GameManager.manager.canCardDivide = true;
    }

    private void NextScene()
    {
        processText.SetText("If you have checked Com's card, let's go for a bet!");
        allReady = true;
        return;
    }

}

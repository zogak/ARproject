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
                //4�� �� �ؽ�Ʈ ��ȯ
                case 4:
                    {
                        processText.SetText("Before dividing the cards, let's decide the betting order.");
                        break;
                    }
                //8�� �� �ؽ�Ʈ ��ȯ
                case 8:
                    {
                        processText.SetText("Touch the arrow!");
                        GameManager.manager.activate = true;
                        break;
                    }
            }
        }
        
        //������ �������� ���� ī�带 ����Ѵٴ� �ؽ�Ʈ�� ��ȯ
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

    //ī�尡 ��еǰ� ���� ���� �ܰ踦 ���� ��ư�� �����޶�� �ؽ�Ʈ�� ��ȯ
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

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

    void Start()
    {
        processText.SetText("Welcome to the world of fun AR card games!");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        num = (int)time;

        switch (num)
        {
            //5�� �� �ؽ�Ʈ ��ȯ
            case 5:
                {
                    processText.SetText("Before dividing the cards, let's decide the betting order.");
                    break;
                }
            //10�� �� �ؽ�Ʈ ��ȯ
            case 10:
                {
                    processText.SetText("Touch the arrow!");
                    break;
                }
        }
        
        //������ �������� ���� ī�带 ����Ѵٴ� �ؽ�Ʈ�� ��ȯ
        if (!(GameSettings.order== -1) && !orderEnd)
        {
            if(GameSettings.order == 0)
            {
                processText.SetText("You will bet first!");
                orderEnd = true;
                Invoke("DividingText", 2);
            }
            else if(GameSettings.order == 1)
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
        GameSettings.canCardDivide = true;
        Invoke("NextScene", 3);
    }

    private void NextScene()
    {
        processText.SetText("If you have checked Com's card, let's go for a bet!");
        allReady = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class GameProcessor : MonoBehaviour
{
    //������ ��ü���� ������ �����ϴ� ��ũ��Ʈ�Դϴ�. ���⼭ �� Ŭ������ �Լ��� Invoke�ϰų� SendMessage�ϴ� ������� ������ �����ŵ�ϴ�.

    //���� ���࿡ �ʿ��� �޼ҵ带 ������ �ִ� ��ũ��Ʈ�� ��� ������ ������ �־�� ��
    private S2ProcessText s2PText;
    private Play2UI p2UI;
    private SpawnObjects spObjects;

    
    void Awake()
    {
        s2PText = FindObjectOfType<S2ProcessText>();
        p2UI = FindObjectOfType<Play2UI>();
        spObjects = FindObjectOfType<SpawnObjects>();
    }
    private void Start()
    {
        GameManager.manager.SendMessage("makeCardNameToInteger");
        GameManager.manager.SendMessage("DividePlayerCard");
        p2UI.ComCardImageUpdate(GameManager.manager.comCardNum);
        //Debug.Log(GameManager.manager.comCardNum);
        //Debug.Log(GameManager.manager.playerCardNum);
    }
    // Update is called once per frame
    void Update()
    {
        //processText �۵���Ű��
        if(spObjects.setBoard == 1)
        {
            if(GameManager.manager.orderNum == 0 && !GameManager.manager.activate && GameManager.manager.currentPlayerState == 0)
            {
                s2PText.SendMessage("ForPlayerText");
            }else if(GameManager.manager.orderNum == 1 && !GameManager.manager.activate && GameManager.manager.currentComState == 0)
            {
                s2PText.SendMessage("ForComText");
            }

            if(GameManager.manager.orderNum == 0 && GameManager.manager.activate && GameManager.manager.currentPlayerState == 2) //player ���� && player acting
            {
                
                if(GameManager.manager.playerChips < GameManager.manager.comBets && GameManager.manager.playerChips <= GameManager.manager.playerBets) //player�� ���� Ĩ ���� ������ com�� ������ Ĩ ������ ���� ��, player�� ���� Ĩ��ŭ ��� �����Ͽ��ٸ�
                {
                    p2UI = FindObjectOfType<Canvas>().GetComponent<Play2UI>();
                    p2UI.okButton.interactable = true;
                    GameManager.manager.activate = false;
                    return;
                }
                else if (TryGetTouchPosition(out Vector2 touchPosition))
                {
                    if (!IsPointerOverUIObject(touchPosition)){
                        spObjects.PlayerActs(touchPosition);
                    }
                }            
            }


        }
    }

    private void LateUpdate()
    {
        
    }

    //��ġ�� �����ϴ� ��
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            //Debug.Log("It's over the object");
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            touchPosition = default;
            return false;
        }
    }

    //��ġ�� ��ġ�� UI���� üũ�ϴ� ��
    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}

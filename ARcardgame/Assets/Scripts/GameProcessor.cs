using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class GameProcessor : MonoBehaviour
{
    //게임의 전체적인 진행을 관리하는 스크립트입니다. 여기서 각 클래스의 함수를 Invoke하거나 SendMessage하는 방식으로 게임을 진행시킵니다.

    //게임 진행에 필요한 메소드를 가지고 있는 스크립트를 모두 변수로 가지고 있어야 함
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
        //processText 작동시키기
        if(spObjects.setBoard == 1)
        {
            if(GameManager.manager.orderNum == 0 && !GameManager.manager.activate && GameManager.manager.currentPlayerState == 0)
            {
                s2PText.SendMessage("ForPlayerText");
            }else if(GameManager.manager.orderNum == 1 && !GameManager.manager.activate && GameManager.manager.currentComState == 0)
            {
                s2PText.SendMessage("ForComText");
            }

            if(GameManager.manager.orderNum == 0 && GameManager.manager.activate && GameManager.manager.currentPlayerState == 2) //player 차례 && player acting
            {
                
                if(GameManager.manager.playerChips < GameManager.manager.comBets && GameManager.manager.playerChips <= GameManager.manager.playerBets) //player의 남은 칩 수가 이전에 com이 베팅한 칩 수보다 작을 때, player가 남은 칩만큼 모두 베팅하였다면
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

    //터치를 감지하는 곳
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

    //터치의 위치가 UI인지 체크하는 곳
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

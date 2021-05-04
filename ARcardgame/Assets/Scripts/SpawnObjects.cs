using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class SpawnObjects : MonoBehaviour
{
    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject board;
    public GameObject playerChip;
    public GameObject comChip;
    public GameObject playerCard;
    public GameObject playerText;
    public GameObject comCard;
    public GameObject comText;
    private GameObject spawned;
    public int setBoard = 0;
    int com_bet = 0; //디폴트
    int player_bet = 0;
    int k = 1;
    private Play2UI p2UI;

    public S2ProcessText process;

    void Start()
    {
        m_RaycastManager = FindObjectOfType<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //turn = GameManager.manager.orderNum;
        if (setBoard == 0) //게임판 instantiate
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                spawned = Instantiate(board, transform.position, transform.rotation);
                spawned.transform.position = transform.position - Vector3.up * (spawned.transform.localScale.y);
                setBoard = 1;
            }

        }

    }

    public void ComActs()
    {
        Debug.Log("comact called");
        com_bet = GameManager.manager.comBets; //manager에서 컴이 베팅하는 수 가져옴
        //fin_bet = false;

        StartCoroutine(ComBetting());
        Debug.Log("coroutine called");
        GameManager.manager.activate = false; //활동 끝

        if(GameManager.manager.comChips == 0)
        {
            if (GameManager.manager.comBets < 3)//베팅 개수에 따라 시간 지연
            {
                Invoke("InvokeFin", 2);
            }
            else if (GameManager.manager.comBets >= 3 && GameManager.manager.comBets < 6)
            {
                Invoke("InvokeFin", 3);
            }
            else if (GameManager.manager.comBets >= 5 && GameManager.manager.comBets < 8)
            {
                Invoke("InvokeFin", 5);
            }
            else if (GameManager.manager.comBets >= 8 && GameManager.manager.comBets < 12)
            {
                Invoke("InvokeFin", 8);
            }
            else if (GameManager.manager.comBets >= 12 && GameManager.manager.comBets < 15)
            {
                Invoke("InvokeFin", 12);
            }
            else if (GameManager.manager.comBets >= 15 && GameManager.manager.comBets < 20)
            {
                Invoke("InvokeFin", 15);
            }
            
        }
        else
        {
            if (GameManager.manager.comBets < 3)//베팅 개수에 따라 시간 지연
            {
                Invoke("InvokeText", 2);
            }
            else if (GameManager.manager.comBets >= 3 && GameManager.manager.comBets < 6)
            {
                Invoke("InvokeText", 3);
            }
            else if (GameManager.manager.comBets >= 5 && GameManager.manager.comBets < 8)
            {
                Invoke("InvokeText", 5);
            }
            else if (GameManager.manager.comBets >= 8 && GameManager.manager.comBets < 12)
            {
                Invoke("InvokeText", 8);
            }
            else if (GameManager.manager.comBets >= 12 && GameManager.manager.comBets < 15)
            {
                Invoke("InvokeText", 12);
            }
            else if (GameManager.manager.comBets >= 15 && GameManager.manager.comBets < 20)
            {
                Invoke("InvokeText", 15);
            }
        }
        
    }

    public void PlayerActs(Vector2 touchPos)
    {
        
        m_RaycastManager.Raycast(touchPos, hits, TrackableType.Planes);
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("board") && GameManager.manager.playerChips >= GameManager.manager.playerBets) //player가 베팅한 칩의 수가 남은 칩의 수보다 작거나 같을 때
            {
                Quaternion qRotation = Quaternion.Euler(0f, 0f, 30f);
                Instantiate(playerChip, hits[0].pose.position + Vector3.up * (transform.localScale.y * 30), qRotation);
                GameManager.manager.playerBets++;
                //GameManager.manager.playerChips -= GameManager.manager.playerBets;

                if (GameManager.manager.playerBets >= GameManager.manager.comBets && GameManager.manager.playerChips>0)
                {
                    p2UI = FindObjectOfType<Canvas>().GetComponent<Play2UI>();
                    p2UI.okButton.interactable = true;
                }
            }
        }
    }

    /*bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            Debug.Log("It's over the object");
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        else if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            Debug.Log("It's over the UI");
            touchPosition = default;
            return false;
        }
        else
        {
            touchPosition = default;
            return false;
        }
    }*/

    public void InvokeFin()
    {
        process.SendMessage("Finish");
    }
    public void InvokeText()
    {
        process.SendMessage("NextTurn");
    }

    IEnumerator ComBetting() //com이 베팅 0.5초마다 칩 1개씩 instantiate
    {
        if (GameManager.manager.activate)
        {
            while (com_bet > 0)
            {
                yield return new WaitForSeconds(0.5f);
                comSpawn();
                com_bet--;

            }
        }
        
    }

    void comSpawn()
    {
        float ranRight = Random.Range(0.1f, -0.1f);
        float ranFor = Random.Range(0.1f, -0.1f);
        Quaternion qRotation = Quaternion.Euler(0f, 0f, 30f);
        Instantiate(comChip, spawned.transform.position + Vector3.up * (transform.localScale.y * 30) + Vector3.right * ranRight + Vector3.forward * ranFor, qRotation);
    }

   public void finalResult() // 서로의 카드 보여주기
    {
        float posPlayerCard = -0.1f;
        float posComCard = 0.1f;
        Quaternion qRotation = Quaternion.Euler(90f, 0f, 0f);
        Instantiate(playerCard, spawned.transform.position + Vector3.up * (transform.localScale.y * 30) + Vector3.right * posPlayerCard, qRotation);
        Instantiate(comCard, spawned.transform.position + Vector3.up * (transform.localScale.y * 30) + Vector3.right * posComCard, qRotation);
        Instantiate(playerText, spawned.transform.position + Vector3.up * (transform.localScale.y * 33) + Vector3.right * posPlayerCard, transform.rotation);
        Instantiate(comText, spawned.transform.position + Vector3.up * (transform.localScale.y * 33) + Vector3.right * posComCard, transform.rotation);

        Invoke("invokeResult", 3);
    }

    void invokeResult()
    {
        p2UI.ResultUI();
    }
}

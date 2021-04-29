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
    private GameObject spawned;
    public int setBoard = 0;
    int com_bet = 0; //����Ʈ
    int player_bet = 0;
    int k = 1;
    //saveValue getTurn;
    //int turn = 3;

    void Start()
    {
        m_RaycastManager = FindObjectOfType<ARRaycastManager>();
        //getTurn = GameObject.Find("Value").GetComponent<saveValue>();
        //turn = GameManager.manager.orderNum;

    }

    // Update is called once per frame
    void Update()
    {
        //turn = GameManager.manager.orderNum;
        if (setBoard == 0) //������ instantiate
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                spawned = Instantiate(board, transform.position, transform.rotation);
                spawned.transform.position = transform.position - Vector3.up * (spawned.transform.localScale.y);
                setBoard = 1;
            }

        }

        else if(setBoard == 1) //������ ���� ����
        {
            if (GameManager.manager.orderNum == 0 && GameManager.manager.activate && GameManager.manager.currentPlayerState == 2) //�÷��̾� Ȱ��
            //if(GameManager.manager.orderNum == 0)
            {
                /*if (TryGetTouchPosition(out Vector2 touchPosition))
                {
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        
                        if (hit.transform.CompareTag("board"))
                        {
                            
                            Quaternion qRotation = Quaternion.Euler(0f, 0f, 30f);
                            Instantiate(playerChip, transform.position + Vector3.up * (transform.localScale.y * 10), qRotation);
                            player_bet++;
                        }
                    }
                    
                }*/
                if (TryGetTouchPosition(out Vector2 touchPosition))
                {
                    m_RaycastManager.Raycast(touchPosition, hits, TrackableType.Planes);
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.CompareTag("board"))
                        {
                            Quaternion qRotation = Quaternion.Euler(0f, 0f, 30f);
                            Instantiate(playerChip, hits[0].pose.position + Vector3.up * (transform.localScale.y * 30), qRotation);
                            player_bet++;
                        }                     
                    }   
                }
                GameManager.manager.playerBets = player_bet; //������ ������Ʈ �� ��ŭ ���ü��� ����� manager�� ����
            }
            else if(GameManager.manager.orderNum == 1 && GameManager.manager.activate) //com Ȱ��
            {
                com_bet = GameManager.manager.comBets; //manager���� ���� �����ϴ� �� ������

                StartCoroutine(ComBetting());
                GameManager.manager.activate = false; //Ȱ�� ��
                
            }
        }

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("I got touch");
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("It's over the object");
                touchPosition = Input.GetTouch(0).position;
                return true;
            }            
        }
        Debug.Log("It's over the UI");
        touchPosition = default;
        return false;
    }

    IEnumerator ComBetting() //com�� ���� 0.5�ʸ��� Ĩ 1���� instantiate
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
}

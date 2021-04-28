using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject board;
    public GameObject playerChip;
    public GameObject comChip;
    private GameObject spawned;
    public int setBoard = 0;
    int com_bet = 0; //디폴트
    int player_bet = 0;
    int k = 1;
    //saveValue getTurn;
    int turn;


    void Start()
    {

        //getTurn = GameObject.Find("Value").GetComponent<saveValue>();
        turn = GameManager.manager.orderNum;
    }

    // Update is called once per frame
    void Update()
    {
        turn = GameManager.manager.orderNum;

        if (setBoard == 0) //게임판 instantiate
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                spawned = Instantiate(board, transform.position, transform.rotation);
                spawned.transform.position = transform.position - Vector3.up * (spawned.transform.localScale.y);
                setBoard = 1;
            }

        }

        else if(setBoard == 1) //게임판 세팅 이후
        {
            if (turn == 0 && GameManager.manager.activate) //플레이어 활동
            {
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    Quaternion qRotation = Quaternion.Euler(0f, 0f, 30f);
                    Instantiate(playerChip, transform.position + Vector3.up * (transform.localScale.y * 10), qRotation);
                    player_bet++;
                }
                GameManager.manager.playerBets = player_bet; //생성된 오브젝트 수 만큼 베팅수로 계산해 manager에 저장
            }
            else if(turn == 1 && GameManager.manager.activate) //com 활동
            {
                com_bet = GameManager.manager.comBets; //manager에서 컴이 베팅하는 수 가져옴

                StartCoroutine(ComBetting());
                GameManager.manager.activate = false; //활동 끝
                //turn = 0; // com betting 후 player turn으로 넘어가기, turn을 GameManager에서 받아올 수 있도록 바꾸고 싶어요!
            }
        }

    }

    IEnumerator ComBetting() //com이 베팅 0.5초마다 칩 1개씩 instantiate
    {
        
        while (com_bet > 0)
        {
            yield return new WaitForSeconds(0.5f);
            comSpawn();
            com_bet--;
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

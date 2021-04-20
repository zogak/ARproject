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
    int com_bat = 5;
    int k = 1;
    saveValue getTurn;
    int turn;


    void Start()
    {

        getTurn = GameObject.Find("Value").GetComponent<saveValue>();
        turn = getTurn.takeTurn;
    }

    // Update is called once per frame
    void Update()
    {

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
            if (turn == 0) //�÷��̾� ��
            {
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    Quaternion qRotation = Quaternion.Euler(0f, 0f, 30f);
                    Instantiate(playerChip, transform.position + Vector3.up * (transform.localScale.y * 10), qRotation);
                }
            }
            else //com ��
            {
                StartCoroutine(ComBetting());
                turn = 0; // com betting �� player turn���� �Ѿ��

            }
        }

    }

    IEnumerator ComBetting() //com�� ���� 0.5�ʸ��� Ĩ 1���� instantiate
    {
        while (com_bat > 0)
        {
            yield return new WaitForSeconds(0.5f);
            comSpawn();
            com_bat--;
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

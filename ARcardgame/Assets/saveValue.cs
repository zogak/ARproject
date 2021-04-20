using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveValue : MonoBehaviour
{
    WhoFirst turn;
    public int takeTurn;
    int init = 0;

    void Start()
    {
        turn = GameObject.Find("Arrow").GetComponent<WhoFirst>();
    }


    void Update()
    {
        if (turn != null&&init==0)
        {
            takeTurn = turn.n;
            init = 1;
        }
        Debug.Log(takeTurn);
    }
}

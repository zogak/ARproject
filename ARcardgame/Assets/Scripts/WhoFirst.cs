using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class WhoFirst : MonoBehaviour
{
    public GameObject first;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    int a = 1;
    public int n;
    
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        n = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(a==1)
        {
            first.transform.Rotate(new Vector3(0, 0, 90));
        }
        else
        {
            if(n==1) //컴퓨터 선
            {
                transform.rotation = Quaternion.Euler(0, -90, 180);
                Destroy(first, 4);
            }
            else //플레이어 선
            {
                transform.rotation = Quaternion.Euler(0, 90, 180);
                Destroy(first, 4);
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            a = 0;
        }
    }
}

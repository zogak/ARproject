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
        //arrow 터치됐는지 확인.
        if(TryGetTouchPosition(out Vector2 touchPosition))
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Arrow"))
                {
                    a = 0;
                }
            }
        }

        if(a==1)
        {
            first.transform.Rotate(new Vector3(0, 0, 90));
        }
        else
        {
            if(n==1) //컴퓨터 선
            {
                GameSettings.order = 1;
                transform.rotation = Quaternion.Euler(0, -90, 180);
                Destroy(first, 4);
            }
            else //플레이어 선
            {
                GameSettings.order = 0;
                transform.rotation = Quaternion.Euler(0, 90, 180);
                Destroy(first, 4);
            }

        }
        /*if (Input.GetMouseButtonDown(0))
        {
            a = 0;
        }*/
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            touchPosition = default;
            return false;
        }
    } 
}

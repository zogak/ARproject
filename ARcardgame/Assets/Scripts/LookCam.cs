using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{

    public Camera target;

    // Update is called once per frame
    void Update()
    {
        Vector3 v = target.transform.position - transform.position;

        v.Normalize();

        Quaternion q = Quaternion.LookRotation(v);

        Quaternion qRotation = Quaternion.Euler(0f, 180f, 0f);
        transform.rotation = q*qRotation;

    }
}

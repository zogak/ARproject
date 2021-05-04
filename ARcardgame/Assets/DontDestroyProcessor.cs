using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyProcessor : MonoBehaviour
{
    void Awake()
    {

        DontDestroyOnLoad(gameObject);

    }
}

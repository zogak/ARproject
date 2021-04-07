using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{

    public GameObject objectToSpawn;

    //private PlacementIndicator indicator;

    // Start is called before the first frame update
    void Start()
    {
        //      indicator = FindObjectOfType<PlacementIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        // spawn only when the first finger has begun touching the screen
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // Instantiate(objectToSpawn, indicator.transform.position, indicator.transform.rotation);
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}

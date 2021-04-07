using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;

    private GameObject indicator;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();

        indicator = transform.GetChild(0).gameObject;
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {

            if (!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
            }

            // move the PlacementIndicator
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

        }
        else
        {
            indicator.SetActive(false);
        }

    }
}

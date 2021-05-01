using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleTracking : MonoBehaviour
{

    private ARTrackedImageManager trackedImgMgr;

    public PlaceablePrefab[] placeablePrefabs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    public ProcessTextChange tc;

    // Start is called before the first frame update
    void Awake()
    {
        trackedImgMgr = FindObjectOfType<ARTrackedImageManager>();

        foreach (PlaceablePrefab pr in placeablePrefabs)
        {
            GameObject go = Instantiate(pr.prefab, Vector3.zero, Quaternion.Euler(-40f, 17f, -124f));
            
            go.name = pr.name;
            spawnedPrefabs.Add(pr.name, go);
            go.SetActive(false);
        }
    }

    public void OnEnable()
    {
        // subscribing to the trackedImageChanged event
        trackedImgMgr.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        // subscribing to the trackedImageChanged event
        trackedImgMgr.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {

        foreach (ARTrackedImage img in args.added)
        {
            UpdateImage(img);
        }

        foreach (ARTrackedImage img in args.updated)
        {
            UpdateImage(img);
        }

        foreach (ARTrackedImage img in args.removed)
        {
            spawnedPrefabs[img.referenceImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage img)
    {

        string imgName = img.referenceImage.name;
        GameObject prefab = spawnedPrefabs[imgName];
        
        //카드를 분배해도 괜찮은 상황(canCardDivide가 true)이면 마커 위에 카드가 뜨게 한다.
        if (img.trackingState == TrackingState.Tracking && GameManager.manager.canCardDivide)
        {
            prefab.transform.position = img.transform.position;
            //prefab.transform.rotation = img.transform.rotation;
            prefab.SetActive(true);
            GameManager.manager.comCard = prefab.GetComponent<Renderer>().material.name;
            Debug.Log(GameManager.manager.comCard);
            tc.SendMessage("NextScene");

        }
        else if (img.trackingState == TrackingState.Limited || img.trackingState == TrackingState.None)
        {
            prefab.SetActive(false);
        }


    }

    [System.Serializable]
    public struct PlaceablePrefab
    {
        public string name;
        public GameObject prefab;
    }
}


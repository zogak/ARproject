using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSound : MonoBehaviour
{
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chip"))
        {
            audio.Play();
        }
    }
}

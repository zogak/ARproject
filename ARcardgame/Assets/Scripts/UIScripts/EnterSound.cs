using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSound : MonoBehaviour
{
    public AudioSource buttonAudio;


    public void SoundEffect()
    {
        buttonAudio.Play();
    }
}

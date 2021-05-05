using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Button back;
    public Slider backVolume;
    public Slider effectVolume;

    public AudioSource backAud;
    public AudioSource effAud;

    private float backVol = 1f;
    private float effVol = 1f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Start()
    {
        backVol = PlayerPrefs.GetFloat("backVol", 1f);
        backVolume.value = backVol;
        backAud.volume = backVolume.value;

        effVol = PlayerPrefs.GetFloat("effVol", 1f);
        effectVolume.value = effVol;
        effAud.volume = effectVolume.value;

    }

    private void Update()
    {
        BackgroundMusic();
        EffectSound();
    }
    private void OnEnable()
    {
        
    }

    public void BackgroundMusic()
    {
        backAud.volume = backVolume.value;

        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backVol", backVol);
    }

    public void EffectSound()
    {
        effAud.volume = effectVolume.value;

        effVol = effectVolume.value;
        PlayerPrefs.SetFloat("effVol", backVol);
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}

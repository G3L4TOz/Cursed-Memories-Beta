using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSong : MonoBehaviour
{
    public AudioClip themeSong; 
    public AudioClip ambientSound; 
    private AudioSource audioSource;
    private AudioSource ambientSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = themeSong;
        audioSource.volume = 0.04f;
        audioSource.Play();
        audioSource.loop = true;
        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.clip = ambientSound;
        ambientSource.volume = 0.15f;
        ambientSource.Play();
        ambientSource.loop = true;
    }

    void Update()
    {
        
    }
}

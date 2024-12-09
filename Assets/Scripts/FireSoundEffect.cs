using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSoundEffect : MonoBehaviour
{
    public float hearingDistance = 5f;
    public float maxVolume = 1f; 
    public AudioSource audioSource;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        AdjustFireVolume();
    }

    void AdjustFireVolume()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < hearingDistance)
        {
            float volume = Mathf.Lerp(0f, maxVolume, 1 - (distance / hearingDistance));
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}

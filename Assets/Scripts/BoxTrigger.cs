using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxTrigger : MonoBehaviour
{
    public GameObject door;
    public GameObject dialouge;
    public GameObject lockdialouge;
    public AudioClip unlockSound; 
    private AudioSource audioSource;
    private bool isNearArea1 = false;
    private bool isNearArea2 = false;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = unlockSound;
        audioSource.playOnAwake = false;
        dialouge.SetActive(false);
        door.SetActive(false);
        lockdialouge.SetActive(true);
    }

    public void Update()
    {
        if (isNearArea1 && isNearArea2)
        {
            PlaySound();
            dialouge.SetActive(true);
            door.SetActive(true);
            Destroy(lockdialouge);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isNearArea1 = true;
        }
        if (other.CompareTag("Box2"))
        {
            isNearArea2 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isNearArea1 = false;
        }
        if (other.CompareTag("Box2"))
        {
            isNearArea2 = false;
        }
    }

    void PlaySound()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
    }
}
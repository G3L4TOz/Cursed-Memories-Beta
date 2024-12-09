using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpScareTrigger : MonoBehaviour
{
    public GameObject jumpScareImage;
    public AudioClip jumpScareSound;
    public GameObject ghostObject;
    public float scareDuration = 3f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = jumpScareSound;
        audioSource.playOnAwake = false;

        jumpScareImage.SetActive(false);
        ghostObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateJumpScare());
        }
    }

    private IEnumerator ActivateJumpScare()
    {
        jumpScareImage.SetActive(true);
        audioSource.Play();

        yield return new WaitForSeconds(scareDuration);

        ghostObject.SetActive(true);
        Destroy(gameObject);
        jumpScareImage.SetActive(false);
    }
}
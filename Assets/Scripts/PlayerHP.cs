using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;    
using Unity.VisualScripting;

public class PlayerHP : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    private int currentHealth;
    public GameObject loseImage;
    public bool isDead = false;
    public bool isHeal = false;
    public AudioClip potionHealingSound;
    public AudioClip crucifixProtectSound;
    public AudioClip deadSound;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource deadaudio;
    private float volume1 = 0.2f;
    private float volume2 = 0.2f;

    public float fadeDuration = 1.0f;
    public float displayDuration = 3.0f;

    private bool isPlaying = false;
    public static PlayerHP instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool isContainShield;
    public GameObject ShieldShow;

    void Start()
    {
        isContainShield = false;
        currentHealth = hearts.Length;
        UpdateHearts();
        loseImage.SetActive(false);

        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = potionHealingSound;
        audioSource1.volume = volume1;
        audioSource1.playOnAwake = false;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = crucifixProtectSound;
        audioSource2.volume = volume2;
        audioSource2.loop = true;
        audioSource2.playOnAwake = false;

        deadaudio = gameObject.AddComponent<AudioSource>();
        deadaudio.clip = deadSound;
        deadaudio.volume = 0.25f;
        deadaudio.playOnAwake = false;
    }

    public void TakeDamage(int damage)
    {
        if (!isContainShield)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, hearts.Length);
            UpdateHearts();
        }

        if (currentHealth <= 0)
        {
            isDead = true;
            loseImage.SetActive(true);
            deadaudio.Play();
            StartCoroutine(LoadSceneAfterDelay(5f));
            if (!isPlaying)
            {
                deadaudio.Play();
                isPlaying = true;
            }
        }

        else
        {
            isContainShield = false;
            audioSource2.Stop();
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 2)
        {
            SceneManager.LoadScene(2);
        }
        else if (currentSceneIndex == 4)
        {
            SceneManager.LoadScene(4);
        }
        else if (currentSceneIndex == 5)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth >= 1 && currentHealth < 3)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, hearts.Length);
            audioSource1.Play();
            UpdateHearts();
            isHeal = true;
        }
        else
        {
            Debug.Log("No Need To Heal!");
            isHeal = false;
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = true;
            }
        }

        for (int i = hearts.Length - 1; i >= currentHealth; i--)
        {
            hearts[i].enabled = false;
        }
    }

    public void CrucifixProtect()
    {
        if (!isContainShield)
        {
            isContainShield = true;
            audioSource2.Play();
        }
        else
        {
            Debug.Log("You already protected by Crucifix");
        }
    }

    void Update()
    {
        ShieldShow.SetActive(isContainShield);
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            FindObjectOfType<PlayerHP>().Heal(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<PlayerHP>().TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            FindObjectOfType<PlayerHP>().CrucifixProtect();
        }*/
    }
}

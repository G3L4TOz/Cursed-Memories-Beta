using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public GameObject fadeCanvas;
    public float fadeDuration = 1.5f;

    private bool isStart = false;
    private bool isCredit = false;

    public AudioClip themeSong; 

    private AudioSource audioSource;

    void Start()
    {
        fadeCanvas.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = themeSong;
        audioSource.volume = 0.04f;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void StartGame()
    {
        isStart = true;
        StartCoroutine(FadeInToBeContinued());
    }

    public void Credit()
    {
        isCredit = true;
        StartCoroutine(FadeInToBeContinued());
    }

    public void QuitGame()
    {
        StartCoroutine(FadeInToBeContinued());
    }

    private IEnumerator FadeInToBeContinued()
    {
        fadeCanvas.SetActive(true);
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        
        fadeCanvasGroup.alpha = 1f;
        if (isStart)
        {
            SceneManager.LoadScene(1);
        }
        else if (isCredit)
        {
            SceneManager.LoadScene(6);
        }
        else
        {
            Application.Quit();
        }
    }
}

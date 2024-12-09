using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutCanvas : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public GameObject fadeCanvas;
    public float fadeDuration = 1.5f;
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        fadeCanvas.SetActive(true);
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration); // ค่อยๆ ลดค่า alpha
            yield return null;
        }
        
        fadeCanvasGroup.alpha = 0f;
        fadeCanvas.SetActive(false);
    }
}

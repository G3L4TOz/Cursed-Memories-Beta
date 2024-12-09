using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialougeMainMenu : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    private int currentDialogueIndex = 0;

    public CanvasGroup fadeCanvasGroup;
    public GameObject fadeCanvas;
    public float fadeDuration = 1.5f;

    void Start()
    {
        dialogueUI.SetActive(false);
        StartCoroutine(FadeOut());
    }

    void Update()
    {
        if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            NextDialogue();
        }
        else if (currentDialogueIndex == 0)
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueUI.SetActive(true);
        currentDialogueIndex = 0;
        dialogueText.text = dialogueLines[currentDialogueIndex];
    }

    void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentDialogueIndex];
        }
        else
        {
            dialogueUI.SetActive(false);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex == 1)
            {
                SceneManager.LoadScene(2);
            }
            else if (currentSceneIndex == 3)
            {
                SceneManager.LoadScene(4);
            }
        }
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
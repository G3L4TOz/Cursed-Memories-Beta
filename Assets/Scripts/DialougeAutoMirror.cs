using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialougeAutoMirror : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;

    private bool isPlayerInRange = false;
    private int currentDialogueIndex = 0;

    private PlayerMovement playerMovement;

    public CanvasGroup toBeContinuedCanvasGroup;
    public GameObject toBeContinuedCanvas;
    public float fadeDuration = 1.5f;

    private Animator animator;

    void Start()
    {
        dialogueUI.SetActive(false);
        toBeContinuedCanvas.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            playerMovement.isDialouge = true;
            if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                NextDialogue();
            }
            else if (currentDialogueIndex == 0)
            {
                StartDialogue();
                animator.SetBool("isTalking", true);
            }
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
            playerMovement.isDialouge = false;
            dialogueUI.SetActive(false);
            StartCoroutine(FadeInToBeContinued());
            animator.SetBool("isTalking", false);
        }
    }

    private IEnumerator FadeInToBeContinued()
    {
        toBeContinuedCanvas.SetActive(true);
        float startAlpha = toBeContinuedCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            toBeContinuedCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        
        toBeContinuedCanvasGroup.alpha = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 2)
        {
            SceneManager.LoadScene(3);
        }
        else if (currentSceneIndex == 4)
        {
            SceneManager.LoadScene(5);
        }
        else if (currentSceneIndex == 5)
        {
            SceneManager.LoadScene(6);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueUI.SetActive(false);
        }
    }
}

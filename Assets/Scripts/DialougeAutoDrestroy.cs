using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeAutoDrestroy : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;

    private bool isPlayerInRange = false;
    private int currentDialogueIndex = 0;

    private PlayerMovement playerMovement;

    void Start()
    {
        dialogueUI.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
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
            Destroy(gameObject);
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

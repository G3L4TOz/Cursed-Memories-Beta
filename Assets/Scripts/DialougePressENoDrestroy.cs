using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougePressENoDrestroy : MonoBehaviour
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
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isDialouge = true;
            if (dialogueUI.activeSelf)
            {
                NextDialogue();
            }
            else
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
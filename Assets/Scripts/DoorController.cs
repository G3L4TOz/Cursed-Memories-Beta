using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorController : MonoBehaviour
{
    public GameObject destroyDialouge;
    public GameObject codeArea;
    public GameObject door;
    public GameObject codeMenuUI;
    public TMP_InputField codeInputField;
    public Button submitButton;
    public TextMeshProUGUI feedbackText;

    public string correctCode = "6974";
    private bool isNearDoor = false;

    void Start()
    {
        codeMenuUI.SetActive(false);
        submitButton.onClick.AddListener(CheckCode);
        feedbackText.gameObject.SetActive(false);
        door.SetActive(false);
    }

    void Update()
    {
        if (isNearDoor)
        {
            codeMenuUI.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            codeMenuUI.SetActive(false);
        }
    }

    void CheckCode()
    {
        string enteredCode = codeInputField.text;

        if (enteredCode == correctCode)
        {
            Destroy(destroyDialouge);
            Destroy(codeArea);
            door.SetActive(true);
            codeMenuUI.SetActive(false);
            feedbackText.gameObject.SetActive(false);
        }
        else
        {
            feedbackText.text = "Incorrect code";
            feedbackText.gameObject.SetActive(true);
        }
    }
}
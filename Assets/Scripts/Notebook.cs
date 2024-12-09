using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notebook : MonoBehaviour
{
    public GameObject notebookUI;
    public TextMeshProUGUI noteText;
    public TextMeshProUGUI titleText;

    private List<NoteData> notes = new List<NoteData>();
    private int currentPage = 0;

    void Start()
    {
        notebookUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (notebookUI.activeSelf)
            {
                CloseNotebook();
            }
            else
            {
                OpenNotebook();
            }
        }
    }

    public void AddNote(string title, string content)
    {
        notes.Add(new NoteData(title, content));
    }

    public void OpenNotebook()
    {
        notebookUI.SetActive(true);
        currentPage = 0;
        UpdateNoteText();
    }

    public void CloseNotebook()
    {
        notebookUI.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < notes.Count - 1)
        {
            currentPage++;
            UpdateNoteText();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateNoteText();
        }
    }

    private void UpdateNoteText()
    {
        if (notes.Count > 0 && currentPage >= 0 && currentPage < notes.Count)
        {
            titleText.text = notes[currentPage].Title;
            noteText.text = notes[currentPage].Content;
        }
        else
        {
            titleText.text = "";
            noteText.text = "";
        }
    }

    private struct NoteData
    {
        public string Title;
        public string Content;

        public NoteData(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}

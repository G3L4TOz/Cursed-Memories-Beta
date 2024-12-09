using UnityEngine;

public class Note : MonoBehaviour
{
    public string noteTitle;
    [TextArea(3, 10)]
    public string noteContent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var notebook = other.GetComponent<Notebook>();
            if (notebook != null)
            {
                notebook.AddNote(noteTitle, noteContent);
                Destroy(gameObject);
            }
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

public class ResetPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<string> boxTags;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetBoxes();
        }
    }

    void ResetBoxes()
    {
        foreach (string tag in boxTags)
        {
            GameObject[] boxes = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject box in boxes)
            {
                BoxReset boxReset = box.GetComponent<BoxReset>();
                if (boxReset != null)
                {
                    box.transform.position = boxReset.InitialPosition;
                }
            }
        }
    }
}

using UnityEngine;

public class BoxSortingLayer : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject boxObject;
    public string frontLayer = "FrontBox";
    public string backLayer = "BackBox";

    void Update()
    {
        UpdateSortingLayer();
    }

    void UpdateSortingLayer()
    {
        SpriteRenderer playerSprite = playerObject.GetComponent<SpriteRenderer>();
        SpriteRenderer boxSprite = boxObject.GetComponent<SpriteRenderer>();

        if (playerObject.transform.position.y > boxObject.transform.position.y + 0.3)
        {
            boxSprite.sortingLayerName = frontLayer;
        }
        else
        {
            boxSprite.sortingLayerName = backLayer;
        }
    }
}

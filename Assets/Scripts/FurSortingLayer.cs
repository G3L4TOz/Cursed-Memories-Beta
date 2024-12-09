using UnityEngine;

public class FurSortingLayer : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject furObject;
    public string frontLayer = "FrontFurniture";
    public string backLayer = "Furniture";

    void Update()
    {
        UpdateSortingLayer();
    }

    void UpdateSortingLayer()
    {
        SpriteRenderer playerSprite = playerObject.GetComponent<SpriteRenderer>();
        SpriteRenderer furSprite = furObject.GetComponent<SpriteRenderer>();

        if (playerObject.transform.position.y > furObject.transform.position.y + 0.8)
        {
            furSprite.sortingLayerName = frontLayer;
        }
        else 
        {
            furSprite.sortingLayerName = backLayer;
        }
    }
}

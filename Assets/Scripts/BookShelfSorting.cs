using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfSorting : MonoBehaviour
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
        SpriteRenderer bookshelfSprite = furObject.GetComponent<SpriteRenderer>();

        if (playerObject.transform.position.y > furObject.transform.position.y - 0.1)
        {
            bookshelfSprite.sortingLayerName = frontLayer;
        }
        else 
        {
            bookshelfSprite.sortingLayerName = backLayer;
        }
    }
}
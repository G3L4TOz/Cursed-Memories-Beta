using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public TextMeshProUGUI itemDisplayText;
    public TextMeshProUGUI itemDisplayTextUse;
    public TextMeshProUGUI itemDisplayTextDrop;
    public Image itemUIImage;

    private ItemType? currentItem = null;
    private Sprite currentItemSprite = null;

    public GameObject swordPrefab;
    public GameObject crucifixPrefab;
    public GameObject holyWaterPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemType itemType, Sprite itemSprite)
    {
        currentItem = itemType;
        currentItemSprite = itemSprite;
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        if (currentItem.HasValue && currentItemSprite != null)
        {
            itemDisplayText.text = currentItem.Value.ToString();
            itemDisplayTextUse.text = "[Space] To Use";
            itemDisplayTextDrop.text = "[Q] To Drop";
            itemUIImage.sprite = currentItemSprite;
            itemUIImage.enabled = true;
        }
        else
        {
            itemDisplayText.text = "";
            itemDisplayTextUse.text = "";
            itemDisplayTextDrop.text = "";
            itemUIImage.sprite = null;
            itemUIImage.enabled = false;
        }
    }

    public ItemType? GetCurrentItem()
    {
        return currentItem;
    }

    public bool HasItem()
    {
        return currentItem.HasValue;
    }

    public void UseItem()
    {
        if (currentItem.HasValue)
        {
            switch (currentItem.Value)
            {
                case ItemType.Sword:
                    Debug.Log("Sword used!");
                    break;
                case ItemType.Crucifix:
                    Debug.Log("Crucifix used!");
                    break;
                case ItemType.HolyWater:
                    Debug.Log("Holy Water used!");
                    break;
            }
            currentItem = null;
            currentItemSprite = null;
            UpdateItemUI();
        }
    }

    public void DropItem()
    {
        if (currentItem.HasValue)
        {
            GameObject itemPrefab = null;
            switch (currentItem.Value)
            {
                case ItemType.Sword:
                    itemPrefab = swordPrefab;
                    break;
                case ItemType.Crucifix:
                    itemPrefab = crucifixPrefab;
                    break;
                case ItemType.HolyWater:
                    itemPrefab = holyWaterPrefab;
                    break;
            }

            if (itemPrefab != null)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
                currentItem = null;
                currentItemSprite = null;
                UpdateItemUI();
            }
        }
    }
}
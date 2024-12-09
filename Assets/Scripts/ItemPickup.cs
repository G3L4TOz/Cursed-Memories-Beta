using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public ItemType itemType;
    public Sprite itemSprite;
    
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            var inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                if (!inventory.HasItem())
                {
                    inventory.AddItem(itemType, itemSprite);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Cannot pick up item, already have an item!");
                }
            }
            else
            {
                Debug.LogError("Player does not have an Inventory component.");
            }
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
        }
    }
}

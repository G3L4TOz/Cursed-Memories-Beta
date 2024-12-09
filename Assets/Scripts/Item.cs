using UnityEngine;

public enum ItemType
{
    Sword,
    Crucifix,
    HolyWater
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public Sprite itemSprite;

    public void Use()
    {
        switch (itemType)
        {
            case ItemType.Sword:
                UseSword();
                break;
            case ItemType.Crucifix:
                UseCrucifix();
                break;
            case ItemType.HolyWater:
                UseHolyWater();
                break;
        }
        Destroy(gameObject);
    }

    private void UseSword()
    {
        Debug.Log("Sword used!");
    }

    private void UseCrucifix()
    {
        Debug.Log("Crucifix used!");
    }

    private void UseHolyWater()
    {
        Debug.Log("Holy Water used!");
    }
}


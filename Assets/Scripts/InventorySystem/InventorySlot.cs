using System.Collections;
using UnityEngine;

[System.Serializable]
public class InventorySlot 
{
    public ItemData itemData;

    public InventorySlot(ItemData itemData)
    {
        this.itemData = itemData;
    }
}
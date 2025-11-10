using System.Collections;
using UnityEngine;

[System.Serializable]
public class InventorySlot 
{
    public ItemDataSO itemData;

    public InventorySlot(ItemDataSO itemData)
    {
        this.itemData = itemData;
    }
}
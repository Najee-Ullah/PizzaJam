using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySlotsAllowed = 4;

    public event EventHandler<OnItemsChangedArgs> OnItemAdded;
    public event EventHandler<OnItemsChangedArgs> OnItemHold;
    public event EventHandler<OnItemsChangedArgs> OnItemRemoved;
    public class OnItemsChangedArgs : EventArgs
    {
        public ItemData changedItem;
    }

    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private int itemIdNo = 0;

    public void AddItem(ItemData item)
    {
            if (inventorySlots.Count < inventorySlotsAllowed)
            {
                item.itemId = itemIdNo++;
                inventorySlots.Add(new InventorySlot(item));
                OnItemAdded?.Invoke(this,new OnItemsChangedArgs {changedItem = item });
            }
    }

    public void RemoveItem(ItemData item)
    {
        InventorySlot existingSlot = inventorySlots.Find(x => x.itemData == item);
        if (existingSlot != null)
        {
            inventorySlots.Remove(existingSlot);
            OnItemRemoved?.Invoke(this, new OnItemsChangedArgs { changedItem = item });
        }

    }

    public int GetInventorySlotsAmount()
    {
        return inventorySlotsAllowed;
    }

    public void OnHoldClicked(ItemData item)
    {
        OnItemHold?.Invoke(this, new OnItemsChangedArgs { changedItem = item });
    }

    public void OnDropClicked(ItemData item)
    {
        RemoveItem(item);
    }

}
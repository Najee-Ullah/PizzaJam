using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySlotsAllowed = 4;

    public event EventHandler<OnItemsChangedArgs> OnItemAdded;
    public event EventHandler<OnItemsChangedArgs> OnItemHold;
    public event EventHandler<OnItemsChangedArgs> OnItemRemoved;
    public event EventHandler<OnItemsReplacedArgs> OnItemsReplaced;
    public class OnItemsChangedArgs : EventArgs
    {
        public ItemDataSO changedItem;
    }
    public class OnItemsReplacedArgs : EventArgs
    {
        public ItemDataSO replacingItem;
        public ItemDataSO replacedItem;
    }
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private int itemIdNo = 0;

    public void AddItem(ItemDataSO item)
    {
            if (inventorySlots.Count < inventorySlotsAllowed)
            {
                item.itemId = itemIdNo++;
                inventorySlots.Add(new InventorySlot(item));
                OnItemAdded?.Invoke(this,new OnItemsChangedArgs {changedItem = item });
            }
    }

    public void RemoveItem(ItemDataSO item)
    {
        InventorySlot existingSlot = inventorySlots.Find(x => x.itemData == item);
        if (existingSlot != null)
        {
            inventorySlots.Remove(existingSlot);
            OnItemRemoved?.Invoke(this, new OnItemsChangedArgs { changedItem = item });
        }

    }

    public void ReplaceItem(ItemDataSO itemDataA, ItemDataSO itemDataB)
    {
        InventorySlot existingSlot = inventorySlots.Find(x => x.itemData == itemDataA);
        if (existingSlot != null)
        {
            inventorySlots.Remove(existingSlot);
            inventorySlots.Add(new InventorySlot(itemDataB));
            OnItemsReplaced.Invoke(this, new OnItemsReplacedArgs { replacedItem = itemDataA,replacingItem = itemDataB });
        }

    }
    public int GetInventorySlotsAmount()
    {
        return inventorySlotsAllowed;
    }

    public void OnHoldClicked(ItemDataSO item)
    {
        OnItemHold?.Invoke(this, new OnItemsChangedArgs { changedItem = item });
    }

    public void OnDropClicked(ItemDataSO item)
    {
        RemoveItem(item);
    }
    public void OnCombineClicked(ItemDataSO item)
    {
        if (ItemCombineManager.Instance.IsActive())
        {
            UpdateItemDataSO(item);
        }
        else
        {
            ItemCombineManager.Instance.CombineStart(item);
        }
    }
    private void UpdateItemDataSO(ItemDataSO item)
    {
        ReplaceItem(item, ItemCombineManager.Instance.CombineEnd(item));
    }
}
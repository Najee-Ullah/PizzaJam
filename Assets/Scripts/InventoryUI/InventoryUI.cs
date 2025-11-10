using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Transform InventoryVisual;
    [SerializeField] TextMeshProUGUI titleBox;
    [SerializeField] TextMeshProUGUI descriptionBox;
    [SerializeField] Transform inventoryBoxPrefab;
    [SerializeField] Transform inventoryBoxParent;
    [SerializeField] Inventory targetInventory;
    [SerializeField] Transform ContextMenuPrefab;

    private List<Transform> inventoryBoxes = new();

    private InputHandler InputSystem;

    private void Start()
    {
        InputSystem = InputHandler.Instance;
        InputSystem.OnInventoryToggleAction += InputSystem_OnInventoryToggleAction;


        inventoryBoxPrefab.gameObject.SetActive(false);

        targetInventory.OnItemAdded += TargetInventory_OnItemAdded;
        targetInventory.OnItemRemoved += TargetInventory_OnItemRemoved;
        targetInventory.OnItemsReplaced += TargetInventory_OnItemsReplaced;

        InitializeInventoryBoxes();
        Hide();
    }

    private void InputSystem_OnInventoryToggleAction(object sender, System.EventArgs e)
    {
        if(InventoryVisual.gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        InventoryVisual.gameObject.SetActive(true);
        SimGameManager.Instance.PauseGame();

    }
    public void Hide()
    {
        InventoryVisual.gameObject.SetActive(false);
        SimGameManager.Instance.StartGame();

        ItemCombineManager.Instance.ResetCombine();

    }

    private void InitializeInventoryBoxes()
    {
        for(int i =0;i<targetInventory.GetInventorySlotsAmount();i++)
        {
            Transform inventoryBoxInstance = Instantiate(inventoryBoxPrefab, inventoryBoxParent);
            inventoryBoxInstance.gameObject.SetActive(true);
            inventoryBoxes.Add(inventoryBoxInstance);
        }
    }

    private void TargetInventory_OnItemRemoved(object sender, Inventory.OnItemsChangedArgs e)
    {
        RemoveUI(e.changedItem);
    }

    private void TargetInventory_OnItemsReplaced(object sender, Inventory.OnItemsReplacedArgs e)
    {
        RemoveUI(e.replacedItem1);
        RemoveUI(e.replacedItem2);
        AddItem(e.replacingItem);
    }

    private void RemoveUI(ItemDataSO itemData)
    {
        foreach (Transform boxTransform in inventoryBoxes)
        {
            InventoryBoxUI boxUI = boxTransform.GetComponent<InventoryBoxUI>();

            if (boxUI.ItemData == itemData)
            {
                boxUI.ClearBox();
                break;
            }
        }
    }

    private void TargetInventory_OnItemAdded(object sender, Inventory.OnItemsChangedArgs e)
    {
        AddItem(e.changedItem);
    }

    private void AddItem(ItemDataSO itemData)
    {
        foreach (Transform boxTransform in inventoryBoxes)
        {
            InventoryBoxUI boxUI = boxTransform.GetComponent<InventoryBoxUI>();
            if (boxUI.ItemData == null)
            {
                boxUI.Initialize(itemData, OnInventoryBoxClicked,OnInventoryBoxRightClicked);
                return;
            }
        }

        Debug.LogWarning("All inventory boxes are occupied — cannot add more items!");

    }


    private void OnInventoryBoxClicked(ItemDataSO itemData)
    {
        titleBox.text = itemData.itemName;
        descriptionBox.text = itemData.itemDescription;
        if(ItemCombineManager.Instance.IsActive())
            targetInventory.OnCombineClicked(itemData);

    }

    private void OnInventoryBoxRightClicked(ItemDataSO itemData,Transform anchorTransform)
    {
        ContextMenuPrefab.GetComponent<InventoryContextMenu>().ShowAtSlot(anchorTransform, itemData, OnHoldClicked, OnDropClicked,OnCombineClick);
    }

    private void OnHoldClicked(ItemDataSO item)
    {
        targetInventory.OnHoldClicked(item);
    }

    private void OnDropClicked(ItemDataSO item)
    {
        targetInventory.OnDropClicked(item);
    }

    private void OnCombineClick(ItemDataSO item)
    {
        targetInventory.OnCombineClicked(item);
    }

}
using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
        foreach (Transform boxTransform in inventoryBoxes)
        {
            InventoryBoxUI boxUI = boxTransform.GetComponent<InventoryBoxUI>();

            if (boxUI.ItemData == e.changedItem)
            {
                boxUI.ClearBox(); 
                inventoryBoxes.Remove(boxTransform);
                break;
            }
        }
    }

    private void TargetInventory_OnItemAdded(object sender, Inventory.OnItemsChangedArgs e)
    {
        AddItem(e.changedItem);
    }

    private void AddItem(ItemData itemData)
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

    private void OnInventoryBoxClicked(ItemData itemData)
    {
        titleBox.text = itemData.itemName;
        descriptionBox.text = itemData.itemDescription;
    }

    private void OnInventoryBoxRightClicked(ItemData itemData,Transform boxTransform)
    {
        //Debug.Log($"Right-clicked (drop): {itemData.itemName}");

        //targetInventory.RemoveItem(itemData);
        ContextMenuPrefab.GetComponent<InventoryContextMenu>().ShowAtSlot(boxTransform, itemData, OnHoldClicked, OnDropClicked);
    }

    private void OnHoldClicked(ItemData item)
    {
        Debug.Log($"Holding {item.itemName}");
    }

    private void OnDropClicked(ItemData item)
    {
        Debug.Log($"Dropping {item.itemName}");
    }


}
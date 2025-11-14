using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class InventoryBoxUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private Transform ContextMenuAnchor;
    [SerializeField] private GameObject RightClickIcon;
    [SerializeField] private GameObject LeftClickIcon;
    private ItemDataSO itemData;

    private Action<ItemDataSO> onLeftClick;
    private Action<ItemDataSO, Transform> onRightClick;

    public ItemDataSO ItemData => itemData;

    private void Start()
    {
        LeftClickIcon.SetActive(true);
        RightClickIcon.SetActive(false);
    }

    public void Initialize(ItemDataSO data,Action<ItemDataSO> leftClick,Action<ItemDataSO,Transform> rightClick = null)
    {
        itemData = data;
        onLeftClick = leftClick;
        onRightClick = rightClick;

        if (iconImage != null)
            if(itemData.itemIcon != null)
                 iconImage.sprite = itemData.itemIcon; 
    }

    public void ClearBox()
    {
        itemData = null;
        if (iconImage != null) iconImage.sprite = defaultImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemData == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeftClick?.Invoke(itemData);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick?.Invoke(itemData,ContextMenuAnchor.transform);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeftClickIcon.SetActive(false);
        RightClickIcon.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RightClickIcon.SetActive(false);
        LeftClickIcon.SetActive(true);
    }
}

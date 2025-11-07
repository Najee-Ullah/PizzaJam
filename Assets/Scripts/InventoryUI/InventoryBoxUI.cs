using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class InventoryBoxUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image iconImage;
    private ItemData itemData;

    private Action<ItemData> onLeftClick;
    private Action<ItemData, Transform> onRightClick;

    public ItemData ItemData => itemData;

    private void Start()
    {
    }

    public void Initialize(ItemData data,Action<ItemData> leftClick,Action<ItemData,Transform> rightClick = null)
    {
        itemData = data;
        onLeftClick = leftClick;
        onRightClick = rightClick;

        if (nameText != null)
            nameText.text = itemData.itemName;

        if (iconImage != null)
            iconImage.sprite = itemData.itemIcon; 
    }

    public void ClearBox()
    {
        itemData = null;
        if (nameText != null) nameText.text = "";
        if (iconImage != null) iconImage.sprite = null;
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
            onRightClick?.Invoke(itemData,transform);
        }
    }
}

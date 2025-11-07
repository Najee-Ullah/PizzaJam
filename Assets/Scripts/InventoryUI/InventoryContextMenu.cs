using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContextMenu : MonoBehaviour
{
    [SerializeField] Button holdButton;
    [SerializeField] Button dropButton;
    [SerializeField] GameObject Visual;

    private ItemData currentItem;
    private Action<ItemData> onHold;
    private Action<ItemData> onDrop;

    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        holdButton.onClick.AddListener(() =>
        {
            onHold?.Invoke(currentItem);
            Hide();
        });

        dropButton.onClick.AddListener(() =>
        {
            onDrop?.Invoke(currentItem);
            Hide();
        });
    }
    public void Show()
    {
        Visual.SetActive(true);
    }
    public void Hide()
    {
        Visual.SetActive(false);
    }
    public void ShowAtSlot(Transform slotTransform, ItemData item, Action<ItemData> holdAction, Action<ItemData> dropAction)
    {
        currentItem = item;
        onHold = holdAction;
        onDrop = dropAction;

        //Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, slotTransform.position);
        //transform.position = screenPos;

        Show();
    }
}

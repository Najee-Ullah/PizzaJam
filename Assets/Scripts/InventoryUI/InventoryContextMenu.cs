using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContextMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Button holdButton;
    [SerializeField] Button dropButton;
    [SerializeField] Button combineButton;
    [SerializeField] GameObject Visual;

    private ItemDataSO currentItem;
    private Action<ItemDataSO> onHold;
    private Action<ItemDataSO> onDrop;
    private Action<ItemDataSO> onCombine;

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

        combineButton.onClick.AddListener(() =>
        {
            onCombine?.Invoke(currentItem);
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
    public void ShowAtSlot(Transform slotTransform, ItemDataSO item, Action<ItemDataSO> holdAction, Action<ItemDataSO> dropAction, Action<ItemDataSO> onCombineAction)
    {
        currentItem = item;
        onHold = holdAction;
        onDrop = dropAction;
        onCombine = onCombineAction;


        transform.SetParent(slotTransform,false);
        transform.localPosition = Vector3.zero;

        Show();
    }
}

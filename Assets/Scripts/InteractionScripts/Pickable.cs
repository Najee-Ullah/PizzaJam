using System.Collections;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField] private ItemDataSO itemData;

    [HideInInspector] public ItemDataSO ItemData => itemData;

    public void Drop(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void PickUp(Transform holdParent)
    {
        transform.parent = holdParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private ItemDataSO GetItemData()
    {
        return itemData;
    }
}
using System.Collections;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField] private ItemData itemData;

    [HideInInspector] public ItemData ItemData => itemData;

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

    private ItemData GetItemData()
    {
        return itemData;
    }
}
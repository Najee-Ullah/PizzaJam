using UnityEngine;

public interface IPickable
{
    ItemDataSO ItemData { get; }
    void PickUp(Transform holdParent);
    void Drop(Transform newParent);
}

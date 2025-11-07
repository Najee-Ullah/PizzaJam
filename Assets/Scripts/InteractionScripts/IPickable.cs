using UnityEngine;

public interface IPickable
{
    ItemData ItemData { get; }
    void PickUp(Transform holdParent);
    void Drop(Transform newParent);
}

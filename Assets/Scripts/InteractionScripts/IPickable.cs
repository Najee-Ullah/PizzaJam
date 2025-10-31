using UnityEngine;

public interface IPickable
{
    void PickUp(Transform holdParent);
    void Drop(Transform newParent);
}

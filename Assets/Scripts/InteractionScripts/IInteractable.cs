using UnityEngine;

public interface IInteractable
{
    ItemDataSO ItemData { get; }
    public void Interact();
}

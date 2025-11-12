using System;
using UnityEngine;

public class TriggerButton : MonoBehaviour,IInteractable
{
    public ItemDataSO ItemData => throw new NotImplementedException();

    public event Action OnButtonPressed;
    public void Interact()
    {
        OnButtonPressed?.Invoke();
    }
}

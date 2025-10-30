using System;
using UnityEngine;

public class TriggerButton : MonoBehaviour,IInteractable
{
    public event Action OnButtonPressed;
    public void Interact()
    {
        OnButtonPressed?.Invoke();
    }
}

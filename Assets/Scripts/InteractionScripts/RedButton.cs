using System;
using UnityEngine;

public class RedButton : MonoBehaviour,IInteractable
{
    public event Action OnButtonPressed;
    public void Interact()
    {
        OnButtonPressed?.Invoke();
    }
}

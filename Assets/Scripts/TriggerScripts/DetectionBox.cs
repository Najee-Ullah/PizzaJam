using System;
using UnityEngine;

public class DetectionBox : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private string targetTag;

    private bool hasTriggered = false;

    public event EventHandler OnPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered || !triggerOnce)
        {
            if (other.CompareTag(targetTag))
            {
                Debug.Log("Player Entered Detection Box");
                hasTriggered = true;
                OnPlayerEnter?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

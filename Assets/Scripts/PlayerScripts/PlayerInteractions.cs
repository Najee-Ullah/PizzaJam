using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("InteractionSettings")]
    [SerializeField] private float interactDistance = 2f;
    public LayerMask interactable;
    [SerializeField] Transform HoldTransform;

    private void Update()
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {

    }
}

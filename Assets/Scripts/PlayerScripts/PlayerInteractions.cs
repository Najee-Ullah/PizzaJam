using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("InteractionSettings")]
    [SerializeField] private float interactDistance = 2f;

    [Header("OutlineLayer")]
    [SerializeField] private string outlineLayerName = "Outline";

    [SerializeField] private string defaultLayerName = "Default";

    [Header("Hold Settings")]
    [SerializeField] Transform HoldTransform;
    [SerializeField] Vector3 heldScale = new Vector3(.1f,.1f,.1f);

    [Header("PlayerInventory")]
    [SerializeField] private Inventory playerInventory;

    private LayerMask detectionMask;
    private int defaultLayer;
    private GameObject currentTarget;
    private Camera cam;

    private bool isHolding
    {
        get { return heldObject != null; }
    }
    private GameObject heldObject;


    private InputHandler InputSystem;

    private void Start()
    {
        InputSystem = InputHandler.Instance;
        InputSystem.OnInteractAction += InputSystem_OnInteractAction;

        detectionMask = LayerMask.GetMask(defaultLayerName, outlineLayerName);
        defaultLayer = LayerMask.NameToLayer(defaultLayerName);
        cam = Camera.main;
    }

    private void InputSystem_OnInteractAction(object sender, System.EventArgs e)
    {
        if (currentTarget != null)
        {
            if (currentTarget.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
            }
            if (!isHolding)
            {
                TryPickUp();
            }
            else
            {
                TryDrop();
            }
        }
    }

    private void Update()
    {
        if(SimGameManager.Instance.IsGamePlaying())
            HandleInteractions();
    }

    private void HandleInteractions()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, detectionMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (currentTarget != hitObject)
            {
                ClearPreviousTarget();
                SetNewTarget(hitObject);
            }
        }
        else
        {
            ClearPreviousTarget();
        }
    }
    private void SetNewTarget(GameObject gameObj)
    {
        currentTarget = gameObj;
        SetLayerRecursively(currentTarget, LayerMask.NameToLayer(outlineLayerName));
    }

    private void ClearPreviousTarget()
    {
        if (currentTarget != null)
        {
            SetLayerRecursively(currentTarget, defaultLayer);
            currentTarget = null;
        }
    }

    private void SetLayerRecursively(GameObject gameObj, int newLayer)
    {
        if (gameObj == null)
            return;
        gameObj.layer = newLayer;
        foreach (Transform child in gameObj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }


    private void TryPickUp()
    {
        if (currentTarget == null)
            return;

        if (currentTarget.TryGetComponent<IPickable>(out IPickable pickable))
        {
            
            if (playerInventory != null)
            {
                playerInventory.AddItem(pickable.ItemData);
            }
            Destroy(currentTarget);
        }
    }
    private void TryDrop()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {

            if (heldObject.TryGetComponent<IPickable>(out IPickable pickable))
            {
                    heldObject = null;
                    if (playerInventory != null)
                    {
                        playerInventory.RemoveItem(pickable.ItemData);
                    }
            }
        }
    }
    private void SetHeldObject(ItemData item)
    {
        if (item == null || item.itemPrefab == null)
            return;

        GameObject itemInstance = Instantiate(item.itemPrefab, HoldTransform);
        itemInstance.transform.localPosition = Vector3.zero;
        itemInstance.transform.localRotation = Quaternion.identity;
        itemInstance.transform.localScale = heldScale;

        if (itemInstance.TryGetComponent<Collider>(out Collider col))
            col.enabled = false;
        if (itemInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
            rb.useGravity = false;

        heldObject = itemInstance;
    }

    private void RemoveHeldObject()
    {
        if (heldObject == null) return;
        Destroy(heldObject);
        heldObject = null;
    }
}
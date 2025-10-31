using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerInteractions : MonoBehaviour
{
    [Header("InteractionSettings")]
    [SerializeField] private float interactDistance = 2f;

    [Header("OutlineLayer")]
    [SerializeField] private string outlineLayerName = "Outline";

    [SerializeField] private string defaultLayerName = "Default";

    [Header("Hold Settings")]
    [SerializeField] Transform HoldTransform;
    [SerializeField] private Vector3 heldScale = Vector3.one * 0.1f;
    [SerializeField] private Vector3 droppedScale = Vector3.one * 0.3f;

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
        if (gameObject == null)
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
            pickable.PickUp(HoldTransform);
            currentTarget.GetComponent<Collider>().enabled = false;
            heldObject = currentTarget;
            heldObject.transform.localScale = heldScale;
            //hideOutlines
            ClearPreviousTarget();
        }
    }
    private void TryDrop()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Transform targetHold = hit.collider.GetComponent<HoldTransformMarker>()?.HoldTransform;

            if (heldObject.TryGetComponent<IPickable>(out IPickable pickable))
            {
                if (targetHold != null)
                    pickable.Drop(targetHold);
                heldObject.GetComponent<Collider>().enabled = true;
                heldObject.transform.localScale = droppedScale;
                heldObject = null;
            }
        }
    }
}
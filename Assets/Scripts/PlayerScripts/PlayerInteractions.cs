using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerInteractions : MonoBehaviour
{
    [Header("InteractionSettings")]
    [SerializeField] private float interactDistance = 2f;

    [Header("OutlineLayer")]
    [SerializeField] private string outlineLayerName = "Outline";

    [SerializeField] private string defaultLayerName = "Default";

    [SerializeField] Transform HoldTransform;

    private LayerMask detectionMask;
    private int defaultLayer;
    private GameObject currentTarget;
    private Camera cam;


    private InputHandler InputSystem;

    private void Start()
    {
        InputSystem = InputHandler.Instance;
        InputSystem.OnInteractAction += InputSystem_OnInteractAction;
        detectionMask = LayerMask.GetMask(defaultLayerName,outlineLayerName);
        defaultLayer = LayerMask.NameToLayer(defaultLayerName);
        cam = Camera.main;
    }

    private void InputSystem_OnInteractAction(object sender, System.EventArgs e)
    {
        if(currentTarget != null)
        {
            if(currentTarget.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void Update()
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance,detectionMask))
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
            SetLayerRecursively(currentTarget,defaultLayer);
            currentTarget = null;
        }
    }

    private void SetLayerRecursively(GameObject gameObj,int newLayer)
    {
        if (gameObject == null)
            return;
        gameObj.layer = newLayer;
        foreach (Transform child in gameObj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

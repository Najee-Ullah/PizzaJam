using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour,IInteractable
{

    public void Interact()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        GetComponent<StateTracker>().SwitchState();
    }
}
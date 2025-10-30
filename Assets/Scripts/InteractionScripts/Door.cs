using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] TriggerButton triggerButton;
    [SerializeField] StateChecker stateChecker;

    private bool isUnlocked = true;

    private void Start()
    {
        if (stateChecker != null) 
            stateChecker.OnCheckStateChanged += Door_OnCheckStateChanged; 

        if(triggerButton != null)
            triggerButton.OnButtonPressed += RedButton_OnButtonPressed;
    }

    private void Door_OnCheckStateChanged(object sender, StateChecker.OnCheckStateChangeEventArgs e)
    {
        isUnlocked = e.state;
    }

    private void RedButton_OnButtonPressed()
    {
        Debug.Log("Door Unlocked : "+isUnlocked);
    }
}

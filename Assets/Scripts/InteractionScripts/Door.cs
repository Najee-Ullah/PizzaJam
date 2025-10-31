using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TriggerButton triggerButton;
    //[SerializeField] private StateChecker stateChecker;--
    //[SerializeField] private const string TRIGGER_NAME = "TRIGGER_NAME";

    private Animator animator;

    private bool isUnlocked = true;

    private void Start()
    {
        animator = GetComponent<Animator>();

      //  if (stateChecker != null) 
            StateChecker.OnCheckStateChanged += Door_OnCheckStateChanged; 

        if(triggerButton != null)
            triggerButton.OnButtonPressed += TriggerButton_OnButtonPressed;
    }

    private void Door_OnCheckStateChanged(object sender, StateChecker.OnCheckStateChangeEventArgs e)
    {
        isUnlocked = e.state == StateChecker.State.Original;
    }

    private void TriggerButton_OnButtonPressed()
    {
        Debug.Log("Door Unlocked : "+isUnlocked);
        //animator.SetTrigger(TRIGGER_NAME);
    }
}

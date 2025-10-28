using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] RedButton redButton;

    private void Start()
    {
        if(redButton != null)
            redButton.OnButtonPressed += RedButton_OnButtonPressed;
    }

    private void RedButton_OnButtonPressed()
    {
        Debug.Log("OpenDoor");
    }
}

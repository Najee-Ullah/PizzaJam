using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateTracker : MonoBehaviour
{
    public StateChecker.State state = StateChecker.State.Original;

    public event EventHandler OnStateChanged;

    [SerializeField] StateType puzzleType;

    private Vector3 defaultRotation;

    private void Start()
    {
        defaultRotation = transform.eulerAngles;
    }

    public enum StateType
    {
        Rotation,//for rotation puzzles
        Presence,//for presence of certain objects on the scene
        OnOff,//for on and off switch puzzles
        Position//for original position based puzzles
    }

    public void SwitchState()
    {
        switch(puzzleType)
        {
            case StateType.Rotation:
                CheckRotation();
                break;
        }

    }
    private void CheckRotation()
    {
        float angleDifference = Quaternion.Angle(
            Quaternion.Euler(defaultRotation),
            transform.rotation
        );

        if(angleDifference < 1f)
        {
            if (state != StateChecker.State.Original)
            {
                state = StateChecker.State.Original;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            if (state != StateChecker.State.Changed)
            {
                state = StateChecker.State.Changed;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }

}

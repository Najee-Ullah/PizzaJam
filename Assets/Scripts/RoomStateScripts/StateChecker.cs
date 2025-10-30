using System;
using System.Collections.Generic;
using UnityEngine;

public class StateChecker : MonoBehaviour
{
    public enum State
    {
        Original,
        Changed
    }

    [SerializeField] private List<StateTracker> stateTrackerList;

    public event EventHandler<OnCheckStateChangeEventArgs> OnCheckStateChanged;
    public class OnCheckStateChangeEventArgs : EventArgs
    {
        public bool state;
    }


    private bool doorsUnlocked = true;

    private void Start()
    {
        foreach (StateTracker tracker in stateTrackerList)
        {
            tracker.OnStateChanged += Tracker_OnStateChanged1;
        }
    }

    private void Tracker_OnStateChanged1(object sender, EventArgs e)
    {
        CheckRoomState();
    }

    private void CheckRoomState()
    {
        doorsUnlocked = true;
        foreach (var tracker in stateTrackerList)
        {
            if(tracker.state != State.Original)
                doorsUnlocked = false;
        }
        OnCheckStateChanged?.Invoke(this, new OnCheckStateChangeEventArgs { state = doorsUnlocked });
    }
}

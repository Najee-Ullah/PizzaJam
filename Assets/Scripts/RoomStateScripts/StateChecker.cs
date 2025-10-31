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

    public static event EventHandler<OnCheckStateChangeEventArgs> OnCheckStateChanged;

    public class OnCheckStateChangeEventArgs : EventArgs
    {
        public State state;
    }

    private State roomState = State.Original;
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
    //unnecessary event call
    private void CheckRoomState()
    {
        roomState = State.Original;
        foreach (var tracker in stateTrackerList)
        {
            if (tracker.state != State.Original)
                roomState = State.Changed;
        }
        OnCheckStateChanged?.Invoke(this, new OnCheckStateChangeEventArgs { state = roomState });
    }
}

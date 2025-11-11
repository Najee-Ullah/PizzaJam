
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnInventoryToggleAction;

    public static InputHandler Instance;

    private InputSystem_Actions PlayerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        PlayerInputActions = new InputSystem_Actions();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += Interact_performed;
        PlayerInputActions.Player.Pause.performed += Pause_performed;
        PlayerInputActions.Player.InventoryToggle.performed += InventoryToggle_performed;
    }

    private void InventoryToggle_performed(InputAction.CallbackContext obj)
    {
        if (SimGameManager.Instance.IsGamePlaying())
        {
            OnInventoryToggleAction?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementInput()
    {
        return PlayerInputActions.Player.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMouseInput()
    {
        return PlayerInputActions.Player.Look.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        PlayerInputActions.Player.Interact.performed -= Interact_performed;
        PlayerInputActions.Player.Pause.performed -= Pause_performed;
        PlayerInputActions.Player.Disable();
    }
}
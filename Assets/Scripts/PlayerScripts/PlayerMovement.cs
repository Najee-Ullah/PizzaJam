using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 90;
    [SerializeField] private float smoothRotationSpeed = 2f;
    [SerializeField] private float minMoveInput = .7f;

    [Header("CharacterCollisionDimensions")]
    [SerializeField] private float playerHeight = .3f;
    [SerializeField] private float playerRadius = 0.7f;

    [Header("Camera Reference")]
    [SerializeField] Camera cam;
    //CameraRotation Lerp
    private float cameraVerticalRotation = 0f;
    private float cameraTargetVerticalRotation = 0f;
    //PlayerRotation Lerp
    private Vector3 playerHorizontalRotation = Vector3.zero;
    private Vector3 playerTargetHorizontalRotation = Vector3.zero;
    //PlayerMovement
    private Vector3 moveDir;

    private InputHandler InputSystem;

    private void Start()
    {
        InputSystem = InputHandler.Instance;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        Vector2 lookInput = InputSystem.GetMouseInput();

        //RotateCameraAroundXAxis
        cameraTargetVerticalRotation -= lookInput.y * mouseSensitivity;
        cameraTargetVerticalRotation = Mathf.Clamp(cameraTargetVerticalRotation, -maxLookAngle, maxLookAngle);
        cameraVerticalRotation = Mathf.Lerp(cameraVerticalRotation,cameraTargetVerticalRotation,smoothRotationSpeed*Time.deltaTime);
        cam.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        //RotateAroundYAxis
        playerTargetHorizontalRotation = Vector3.up * lookInput.x * mouseSensitivity;
        playerHorizontalRotation = Vector3.Lerp(playerHorizontalRotation, playerTargetHorizontalRotation, smoothRotationSpeed * Time.deltaTime);
        transform.Rotate(playerHorizontalRotation);

    }

    private void HandleMovement()
    {
        Vector2 moveInput = InputSystem.GetMovementInput();
        moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, movementSpeed * Time.deltaTime);
        if (!canMove)
        {
            //Capsule Cast Params
            Vector3 start = transform.position;
            Vector3 end = start + Vector3.up * playerHeight;

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = Mathf.Abs(moveDir.x) >=minMoveInput && !Physics.CapsuleCast(start, end, playerRadius, moveDirX, movementSpeed * Time.deltaTime);
            if (canMove)
            {
                moveDir = moveDirX.normalized;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = Mathf.Abs(moveDir.z) >= minMoveInput && !Physics.CapsuleCast(start, end, playerRadius, moveDirZ, movementSpeed * Time.deltaTime);
                if (canMove)
                {
                    moveDir = moveDirZ.normalized;
                }
            }
        }
        if (moveInput.magnitude>0f && canMove)
        {
            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * playerHeight);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, playerRadius);
    }


}



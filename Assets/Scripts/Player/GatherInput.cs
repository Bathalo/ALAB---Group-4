using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{

    public PlayerControls myControls;

    //X axis input direction
    public float valueX;

    //Identify Jump Input
    public bool jumpInput;

    //Identify Interact Input
    public bool interactInput;

    private void Awake()
    {
        myControls = new PlayerControls();
    }

    private void OnEnable()
    {
        myControls.Player.Move.performed += StartMove;
        myControls.Player.Move.canceled += StopMove;

        myControls.Player.Jump.performed += JumpStart;
        myControls.Player.Jump.canceled += JumpStop;

        myControls.Player.Interact.performed += InteractStart;
        myControls.Player.Interact.canceled += InteractStop;

        myControls.Player.Enable();
    }

    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;

        myControls.Player.Interact.performed -= InteractStart;
        myControls.Player.Interact.canceled -= InteractStop;

        myControls.Player.Disable();
    }

    private Vector2 moveInput; // Declare it here

    private void StartMove(InputAction.CallbackContext ctx)
    {
    moveInput = ctx.ReadValue<Vector2>(); // Read the Vector2 input value
    //valueX = moveInput.x; // Extract the X component of the Vector2 
    
    // FLIP KEY 'A' AND 'D'
    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
    {
        valueX = 1;
    }
    else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
    {
        valueX = -1;
    }
    else
    {
        valueX = 0;
    }
    
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }

    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }

    private void InteractStart(InputAction.CallbackContext ctx)
    {
        interactInput = true;

        Debug.Log("Interact button pressed");
    }

    private void InteractStop(InputAction.CallbackContext ctx)
    {
        interactInput = false;

        Debug.Log("Interact button released"); 
    }
}
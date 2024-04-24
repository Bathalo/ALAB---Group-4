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

        myControls.Player.Enable();
    }

    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;

        myControls.Player.Disable();
    }

    private Vector2 moveInput; // Declare it here

    private void StartMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>(); // Read the Vector2 input value
        valueX = moveInput.x; // Extract the X component of the Vector2 input
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

}
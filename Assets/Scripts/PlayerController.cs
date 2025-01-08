using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;

    public event Action<Vector2> OnMove;
    public event Action OnStop;

    private Vector2 movementInput;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput playerInput;

    void OnEnable()
    {
        // Subscribe to the input action events
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        // Unsubscribe from the input action events
        playerInput.actions["Move"].performed -= OnMovePerformed;
        playerInput.actions["Move"].canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read the movement input
        movementInput = context.ReadValue<Vector2>();

        // Notify listeners about the movement
        OnMove?.Invoke(movementInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset movement input
        movementInput = Vector2.zero;

        // Notify listeners about stopping movement
        OnStop?.Invoke();
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody
        rb.linearVelocity = movementInput * moveSpeed;
    }
}

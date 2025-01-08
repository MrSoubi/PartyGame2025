using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerShooting playerShooting;

    private void OnEnable()
    {
        // Subscribe to events from PlayerController
        playerController.OnMove += HandleMove;
        playerController.OnStop += HandleStop;

        playerShooting.OnShoot += PlayShootAnimation;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        playerController.OnMove -= HandleMove;
        playerController.OnStop -= HandleStop;

        playerShooting.OnShoot -= PlayShootAnimation;
    }

    private void HandleMove(Vector2 movementInput)
    {
        // Walking animation
        animator.SetBool("IsWalking", true);

        // Flip the sprite based on movement direction (left or right)
        if (movementInput.x != 0)
        {
            spriteRenderer.flipX = movementInput.x < 0;
        }
    }

    private void HandleStop()
    {
        // Idle animation
        animator.SetBool("IsWalking", false);
    }

    public void PlayShootAnimation()
    {
        // Trigger the shooting animation
        animator.SetTrigger("Shoot");
    }

    public void PlayDeathAnimation()
    {
        // Trigger the death animation
        animator.SetTrigger("Die");
    }
}

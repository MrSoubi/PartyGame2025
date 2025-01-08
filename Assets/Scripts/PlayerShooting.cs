using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed = 10f;

    public event Action OnShoot;

    private float lastShootTime = -1f;
    private const float shootCooldown = 0.25f;

    private void OnEnable()
    {
        // Subscribe to the input action event
        playerInput.actions["Attack"].performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the input action event
        playerInput.actions["Attack"].performed -= OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    public void Shoot()
    {
        if (projectilePrefab == null || shootingPoint == null) return;

        // Trigger the shoot event
        OnShoot?.Invoke();

        // Determine the direction based on sprite orientation
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // Adjust the shooting point for left direction relative to the player's position
        Vector3 spawnPosition = shootingPoint.position;
        if (spriteRenderer.flipX)
        {
            spawnPosition.x -= 0.5f;
        }
        else
        {
            spawnPosition.x += 0.5f;
        }

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Set the velocity of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}

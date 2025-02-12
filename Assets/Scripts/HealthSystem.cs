using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f; // Maximum health of the character
    private float currentHealth; // Current health of the character

    // Events for handling damage, health restoration, and death
    public event Action OnDeath;
    public event Action<float> OnDamageTaken;
    public event Action<float> OnHealthRestored;

    void Awake()
    {
        // Initialize current health to maximum health
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        if (damage <= 0 || currentHealth <= 0) return; // Prevents negative values or damage when already dead

        currentHealth -= damage;
        OnDamageTaken?.Invoke(currentHealth); // Trigger the damage taken event

        if (currentHealth <= 0)
        {
            Die(); // If health reaches 0, the character dies
        }
    }

    // Method to restore health
    public void RestoreHealth(float amount)
    {
        if (amount <= 0 || currentHealth <= 0) return; // Prevents restoring invalid values or reviving dead characters

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // Ensures health does not exceed maximum health
        OnHealthRestored?.Invoke(currentHealth); // Trigger the health restored event
    }

    // Method that handles character death
    public void Die()
    {
        OnDeath?.Invoke(); // Trigger the death event
        Destroy(gameObject); // Destroy the character (can be replaced with animations or other behaviors)
    }

    // Method to set a new maximum health value
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth; // Reset current health to the new maximum health
    }

    // Method to set the current health manually
    public void SetCurrentHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth); // Ensures health stays within valid range
    }

    // Method to get the current health
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // Method to get the maximum health
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

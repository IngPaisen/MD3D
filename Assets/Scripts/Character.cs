using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        // Subscribe to the health events
        healthSystem.OnDeath += HandleDeath;
        healthSystem.OnDamageTaken += HandleDamage;
        healthSystem.OnHealthRestored += HandleHealthRestoration;
    }


    // Update is called once per frame
    void Update()
    {
        // Example inputs to test damage and healing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.TakeDamage(10f);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.RestoreHealth(5f);
        }
    }

    private void HandleDamage(float currentHealth)
    {
        Debug.Log($"Character took damage! Current health: {currentHealth}");
    }

    private void HandleHealthRestoration(float currentHealth)
    {
        Debug.Log($"Character restored health! Current health: {currentHealth}");
    }

    private void HandleDeath()
    {
        Debug.Log("Character died!");
        // Additional death logic (e.g., play animation, disable movement, etc.)
    }
}

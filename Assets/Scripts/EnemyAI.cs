using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player
    public Transform player;

    // Configurable enemy parameters
    public float moveSpeed = 3.5f; // Movement speed
    public float attackRange = 2f; // Attack range
    public float attackCooldown = 1.5f; // Time between attacks
    public float damage = 10f; // Damage dealt per attack

    // Reference to the HealthSystem component
    private HealthSystem healthSystem;

    // Private variables
    private NavMeshAgent agent; // Navigation component
    private float lastAttackTime; // Track last attack time

    // Start is called before the first frame update
    void Start()
    {
        // Get the NavMeshAgent component and set movement speed
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // Get the HealthSystem component
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // If no player is assigned, do nothing
        if (player == null) return;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // If the player is out of attack range, move towards them
            agent.SetDestination(player.position);
        }
        else
        {
            // If within attack range, stop moving
            agent.ResetPath();

            // Check if enough time has passed since the last attack
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time; // Update last attack time
            }
        }
    }

    void Attack()
    {
        // Check if the player has a HealthSystem component
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();

        if (playerHealth != null)
        {
            // Apply damage to the player
            playerHealth.TakeDamage(damage);

            Debug.Log("Enemy attacks and deals " + damage + " damage to the player!");
        }
        else
        {
            Debug.LogWarning("Player does not have a HealthSystem component!");
        }
    }
}

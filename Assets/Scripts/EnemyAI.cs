using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;          // Assign the player's Transform in the Inspector
    private NavMeshAgent agent;       // NavMeshAgent to handle movement

    public float detectionRange = 10f; // Range to detect the player
    public float health = 100f;       // Enemy health

    // Patrol settings
    public float patrolRange = 20f;   // Radius for random patrol points
    public float patrolWaitTime = 2f; // Time to wait at each patrol point
    private bool isPatrolling = false;
    private Vector3 patrolPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartPatrol(); // Start the patrolling behavior
    }

    void Update()
    {
        // Check if the player is within detection range
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            StopPatrol(); // Stop patrolling
            agent.SetDestination(player.position); // Move towards the player
        }
        else
        {
            // Continue patrolling if the player is out of range
            if (!isPatrolling)
            {
                StartPatrol();
            }
            Patrol();
        }
    }

    // Start patrolling behavior
    private void StartPatrol()
    {
        isPatrolling = true;
        SetRandomPatrolPoint();
    }

    // Patrol to the assigned point
    private void Patrol()
    {
        // Check if the enemy has reached the patrol point
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(WaitBeforeNextPatrol());
        }
    }

    // Stop patrolling behavior
    private void StopPatrol()
    {
        isPatrolling = false;
        StopCoroutine(WaitBeforeNextPatrol());
    }

    // Set a random patrol point within the patrol range
    private void SetRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            agent.SetDestination(patrolPoint);
        }
    }

    // Wait for a short time before moving to the next patrol point
    private IEnumerator WaitBeforeNextPatrol()
    {
        isPatrolling = false;
        yield return new WaitForSeconds(patrolWaitTime);
        SetRandomPatrolPoint();
        isPatrolling = true;
    }

    // Function to take damage
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0f)
        {
            Die(); // Call Die function when health is 0 or less
        }
    }

    // Function to handle enemy death
    private void Die()
    {
        Destroy(gameObject); // Destroy the enemy GameObject
        Debug.Log("Enemy has been defeated!");
    }
}

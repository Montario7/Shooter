using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;           // Player's Transform
    private NavMeshAgent agent;        // NavMeshAgent for movement

    [Header("Enemy Settings")]
    public float detectionRange = 10f; // Enemy detection range
    public float attackRange = 5f;     // Enemy attack range
    public float health = 100f;        // Enemy health
    public float attackDamage = 10f;   // Damage dealt to the player
    public float attackCooldown = 2f; // Time between attacks

    private float lastAttackTime = 0f; // Tracks time since the last attack

    [Header("Patrol Settings")]
    public float patrolRange = 20f;    // Radius for random patrol points
    public float patrolWaitTime = 2f;  // Time to wait at each patrol point
    private bool isPatrolling = false;
    private Vector3 patrolPoint;       // Current patrol target point
    private bool playerInRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartPatrol(); // Start patrolling initially
    }

    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInRange = distanceToPlayer <= detectionRange;

        if (playerInRange)
        {
            ChasePlayer();

            // Check if within attack range
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            if (!isPatrolling)
            {
                StartPatrol();
            }
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        StopPatrol(); // Stop patrolling
        agent.SetDestination(player.position); // Move towards player
    }

    private void AttackPlayer()
    {
        // Only attack if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage); // Reduce player health
                Debug.Log($"Enemy attacked the player for {attackDamage} damage!");
            }

            lastAttackTime = Time.time; // Reset attack timer
        }
    }

    private void StartPatrol()
    {
        isPatrolling = true;
        SetRandomPatrolPoint();
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(WaitBeforeNextPatrol());
        }
    }

    private void StopPatrol()
    {
        isPatrolling = false;
        StopAllCoroutines(); // Stop any ongoing patrol routines
    }

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

    private IEnumerator WaitBeforeNextPatrol()
    {
        isPatrolling = false;
        yield return new WaitForSeconds(patrolWaitTime);
        SetRandomPatrolPoint();
        isPatrolling = true;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has been defeated!");
        Destroy(gameObject); // Destroy enemy GameObject
    }
}

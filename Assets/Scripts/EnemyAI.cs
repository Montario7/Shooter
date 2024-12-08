using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [Header("Enemy Settings")]
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float health = 100f;
    public float attackDamage = 10f;
    public float attackCooldown = 2f;

    private float lastAttackTime = 0f;

    [Header("Patrol Settings")]
    public float patrolRange = 20f;
    public float patrolWaitTime = 2f;
    private bool isPatrolling = false;
    private Vector3 patrolPoint;
    private bool playerInRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartPatrol();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInRange = distanceToPlayer <= detectionRange;

        if (playerInRange)
        {
            ChasePlayer();

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
        StopPatrol();
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"Enemy attacked the player for {attackDamage} damage!");
            }

            lastAttackTime = Time.time;
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
        StopAllCoroutines();
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
        Destroy(gameObject);
    }
}

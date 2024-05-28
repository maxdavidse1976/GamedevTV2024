using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_AIMovement : MonoBehaviour
{
    Player player;
    Enemy enemyCS;
    NavMeshAgent navAgent;
    float distanceToPlayer;
    float distanceToTower;

    void Awake()
    {
        enemyCS = GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        player = Player.Instance;
        
        navAgent.speed = enemyCS.moveSpeed;
        navAgent.stoppingDistance = enemyCS.stoppingDistance;

        enemyCS.canMove = true;
    }

    void Update()
    {
        if (enemyCS.IsDead()) navAgent.isStopped = true;

        if (!player || !enemyCS.canMove || enemyCS.IsDead()) return;

        distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);
        distanceToTower = Vector3.Distance(Vector3.zero, transform.position);

        if (distanceToPlayer <= enemyCS.chaseRange || distanceToPlayer < distanceToTower)
        {
            enemyCS.currentTarget = Player.Instance.transform.position;
            MoveAI(enemyCS.currentTarget);
        }
        else
        {
            enemyCS.currentTarget = Vector3.zero;
            MoveAI(Vector3.zero);
        }

        enemyCS.animator.PlayMovementAnimations(enemyCS.canMove);
    }

    void MoveAI(Vector3 _target)
    {
        navAgent.SetDestination(_target);
        Vector3 lookDir = new Vector3(_target.x, transform.position.y, _target.z);
        transform.LookAt(lookDir);
    }
}

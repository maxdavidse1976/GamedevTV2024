using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_AIMovement : MonoBehaviour
{
    Enemy enemyCS;
    NavMeshAgent navAgent;
    float distanceToPlayer;

    void Start()
    {
        enemyCS = GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.speed = enemyCS.moveSpeed;
        navAgent.stoppingDistance = enemyCS.stoppingDistance;
    }

    void Update()
    {
        // Calculate the distance to the player
        distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);

        // Check if the player is within chase range
        if (distanceToPlayer <= enemyCS.chaseRange)
        {
            // Set the agent's destination to the player's position
            navAgent.SetDestination(Player.Instance.transform.position);
        }
        else
        {
            // Stop the agent from moving if the player is out of range
            navAgent.SetDestination(Vector3.zero);
        }
    }
}

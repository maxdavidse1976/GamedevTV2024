using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Health
{
    [Header("Movement Settings")]
    public float stoppingDistance = 2.5f;
    public float moveSpeed = 4f;
    public float chaseRange = 7f; // Range within which the enemy will start chasing the player

    [Header("Damage Settings")]
    public LayerMask damageableLayers;
    public float attackRange;
    public float attackRate;
    public int damage;

    [Header("Other References")]
    public Vector3 currentTarget;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RegisterEnemy(this);
        }
    }

    void Update()
    {
        if (healthBar) healthBar.value = currentHealth;
    }

    private void OnDisable()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }

    private void OnDestroy()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }
}

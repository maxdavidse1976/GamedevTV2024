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
    public bool canMove = true;

    [Header("Damage Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public LayerMask damageableLayers;
    public float projectileSpeed = 15f;
    public float bulletLife = 1f;
    public float attackRange;
    public float attackRate;
    public int damage;

    [Header("Other References")]
    public Vector3 currentTarget;
    public Enemy_AnimatorController animator;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RegisterEnemy(this);
        }
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

    protected override void Die()
    {
        StartCoroutine(DeathTimeCo(1.5f));
    }

    IEnumerator DeathTimeCo(float _seconds)
    {
        animator.PlayDeathAnimation();
        yield return new WaitForSeconds(_seconds);
        base.Die();
    }
}

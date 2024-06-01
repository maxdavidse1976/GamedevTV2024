using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Health
{
    public static Player Instance;

    [Header("Movement Settings")] 
    public float moveSpeed = 5f;

    [Header("Gun Settings")] 
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int damage = 50;
    public float fireRate = 0.5f;
    public float projectileSpeed = 20f;
    public float bulletRange = 5f;
    public Image crosshair;

    [Header("Player Effects Settings")]
    [SerializeField] string _deathEffect;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Die()
    {

        if (!isDead)
            PoolManager.Instance.SpawnFromPool(_deathEffect, transform.position, Quaternion.identity);

        Player_Animator.Instance.PlayDeathAnimation();
        GameManager.Instance.EndGame();
        base.Die();
    }
}

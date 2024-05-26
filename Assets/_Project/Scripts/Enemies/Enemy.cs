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
    public float attackRate;
    public float damage;
    public float damageDistance;

    [Header("UI Settings")]
    [SerializeField] Slider healthBar;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (healthBar)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (healthBar) healthBar.value = currentHealth;
    }
}

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
    public float bulletLife = 0.5f;

    [Header("UI Settings")]
    [SerializeField] Slider healthBar;
    public Image crosshair;

    private void Awake()
    {
        Instance = this;
    }

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

        if(healthBar) healthBar.value = currentHealth;
    }
}

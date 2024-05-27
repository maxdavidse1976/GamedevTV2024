using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Health
{
    public static Tower Instance;

    [Header("Relic settings")] 
    [SerializeField] int _damage = 100;
    [SerializeField] float _fireRate = 0.05f;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _firePoint;
    [SerializeField] float _projectileSpeed = 20f;
    [SerializeField] float _bulletLife = 0.5f;

    [Header("Attack settings")] 
    [SerializeField] float _attackRadius = 20f;
}

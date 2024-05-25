using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot(Player.Instance.bulletPrefab,Player.Instance.firePoint, Player.Instance.projectileSpeed);
            nextFireTime = Time.time + Player.Instance.fireRate;
        }
    }

    void Shoot(GameObject _bulletPrefab,Transform _firepoint, float _projectileSpeed)
    {
        GameObject projectile = Instantiate(_bulletPrefab, _firepoint.position, _firepoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = _firepoint.forward * _projectileSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    enum AttackType { Melee,Ranged }

    [SerializeField] AttackType attackType;
    Enemy enemyCS;
    float nextFireTime = 0f;

    private void Start()
    {
        enemyCS = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemyCS.IsDead()) return;

        float distanceToTarget = Vector3.Distance(enemyCS.currentTarget, transform.position);

        if (Time.time >= nextFireTime && distanceToTarget <= enemyCS.attackRange)
        {
            enemyCS.canMove = false;
            enemyCS.animator.PlayAttackAnimation();
            Invoke("Attack", 0.2f);
            nextFireTime = Time.time + Player.Instance.fireRate;
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * enemyCS.attackRange, Color.red);
    }

    void Attack()
    {
        if (attackType == AttackType.Melee) Melee();
        else if (attackType == AttackType.Ranged) Shoot(enemyCS.bulletPrefab, enemyCS.firePoint, enemyCS.projectileSpeed);

        Invoke("RestartMoving", .5f);
    }

    void RestartMoving()
    {
        enemyCS.canMove = true;
    }

    void Shoot(GameObject _bulletPrefab, Transform _firepoint, float _projectileSpeed)
    {
        GameObject projectile = Instantiate(_bulletPrefab, _firepoint.position, _firepoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = _firepoint.forward * _projectileSpeed;
    }

    void Melee()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, fwd, enemyCS.damageableLayers);

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(enemyCS.damage);
            }
        }
    }
}

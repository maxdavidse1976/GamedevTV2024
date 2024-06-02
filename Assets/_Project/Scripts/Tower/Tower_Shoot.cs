using UnityEngine;

public class Tower_Shoot : MonoBehaviour
{
    float nextFireTime = 0f;

    Transform nearestTarget;

    void Update()
    {
        if (!EnemyManager.Instance.waveStarted || Player.Instance.IsDead()) return;

        RaycastHit[] hits = Physics.SphereCastAll(Tower.Instance._firePoint.position, Tower.Instance._bulletRange, Vector3.up);
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position,transform.position);
                float lastNearestDistance = 1000f;
                if (nearestTarget)
                {
                    lastNearestDistance = Vector3.Distance(nearestTarget.transform.position, transform.position);
                }

                if(distanceToEnemy < lastNearestDistance)
                {
                    nearestTarget = enemy.transform;
                }
            }
        }

        if (nearestTarget && Time.time >= nextFireTime && EnemyManager.Instance.waveStarted)
        {
            Shoot(Tower.Instance._bulletPrefab, Tower.Instance._firePoint, Tower.Instance._projectileSpeed);
            nextFireTime = Time.time + Tower.Instance._fireRate;
        }
    }

    void Shoot(GameObject _bulletPrefab, Transform _firepoint, float _projectileSpeed)
    {
        GameObject projectile = Instantiate(_bulletPrefab, _firepoint.position, _firepoint.rotation);
        projectile.GetComponent<Bullet>().InitializeProjectile(BulletOwner.Tower);

        Vector3 lookDir = new Vector3(-nearestTarget.position.x, nearestTarget.position.y, nearestTarget.position.z);
        projectile.transform.LookAt(nearestTarget);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = rb.transform.forward * _projectileSpeed;
    }
}

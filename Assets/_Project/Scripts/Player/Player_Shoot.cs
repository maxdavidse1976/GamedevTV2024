using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    float nextFireTime = 0f;

    void Update()
    {
        if (Player.Instance.IsDead()) return;

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Player_Animator.Instance.PlayShootAnimation();
            Invoke("ShootAtMouse",.2f);
            nextFireTime = Time.time + Player.Instance.fireRate;
        }
    }

    public void Shoot(GameObject bulletPrefab, Transform firepoint, float projectileSpeed)
    {
        GameObject projectile = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        projectile.GetComponent<Bullet>().InitializeProjectile(BulletOwner.Player);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100); // 100 units away from the camera
        }

        Vector3 direction = (targetPoint - firepoint.position).normalized;

        projectile.transform.rotation = Quaternion.LookRotation(direction);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * projectileSpeed;
    }



    void ShootAtMouse()
    {
        Shoot(Player.Instance.bulletPrefab, Player.Instance.firePoint, Player.Instance.projectileSpeed);
    }
}

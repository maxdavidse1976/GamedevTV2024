using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    float nextFireTime = 0f;
    float yRotOffset = 50f;

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

        // Convert mouse position to world position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            // Fallback if raycast doesn't hit anything, you can define a default distance
            targetPoint = ray.GetPoint(100); // 100 units away from the camera
        }

        // Calculate direction only considering y-axis rotation
        Vector3 direction = (targetPoint - firepoint.position).normalized;
        direction.y = 0; // Flatten direction to ensure rotation only on the y-axis

        // Rotate projectile to look at the target direction on the y-axis
        projectile.transform.rotation = Quaternion.LookRotation(direction);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Set velocity towards the target point
        rb.velocity = direction.normalized * projectileSpeed;
    }

    void ShootAtMouse()
    {
        Shoot(Player.Instance.bulletPrefab, Player.Instance.firePoint, Player.Instance.projectileSpeed);
    }
}

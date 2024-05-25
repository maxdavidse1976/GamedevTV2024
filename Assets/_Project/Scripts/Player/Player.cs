using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int health;
    public float moveSpeed = 5f;

    public GameObject bulletPrefab;
    public UnityEngine.UI.Image crosshair;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float projectileSpeed = 20f;
    public float bulletLife = 0.5f;

    private void Awake()
    {
        Instance = this;
    }
}

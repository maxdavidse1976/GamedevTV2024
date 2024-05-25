using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int bulletDamage;
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private Transform bulletTF;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        bulletRB = GetComponent<Rigidbody>();
        bulletTF = GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        LaunchProjectile();
    }

    protected virtual void LaunchProjectile()
    {
        bulletRB.velocity = bulletTF.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        HandleCollision(_collider);
    }

    protected virtual void HandleCollision(Collider _collider)
    {
        if (_collider.TryGetComponent(out Player _health))
        {
            _health.TakeDamage(bulletDamage);
        }

        DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
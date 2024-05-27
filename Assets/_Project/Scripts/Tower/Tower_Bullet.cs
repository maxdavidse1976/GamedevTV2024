using UnityEngine;

public class Tower_Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider _collider)
    {
        HandleCollision(_collider);
    }

    protected virtual void HandleCollision(Collider _collider)
    {
        if (_collider.TryGetComponent(out Enemy _health))
        {
            _health.TakeDamage(Tower.Instance._damage);
        }

        DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider _collider)
    {
        HandleCollision(_collider);
    }

    protected virtual void HandleCollision(Collider _collider)
    {
        if (_collider.TryGetComponent(out Health _health))
        {
            if(!_health.IsPlayer())
            _health.TakeDamage(Player.Instance.damage);
        }

        DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletOwner
{
    Player, Tower, Enemy
}

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletOwner bulletOwner;
    [SerializeField] float range;
    [SerializeField] string hitEffectTag;

    Vector3 startPos;

    public void InitializeProjectile(BulletOwner _owner)
    {
        bulletOwner = _owner;

        if(bulletOwner == BulletOwner.Player) { range = Player.Instance.bulletRange; }
        else if (bulletOwner == BulletOwner.Tower) { range = Tower.Instance._bulletRange; }
        else { range = 10f; }
    }

    private void OnEnable()
    {
        startPos = transform.position;
    }

    void LateUpdate()
    {
        RangeCheck();
    }

    void OnTriggerEnter(Collider _collider)
    {
        HandleCollision(_collider);
    }

    void HandleCollision(Collider _collider)
    {
        if (bulletOwner == BulletOwner.Player)
        {
            if (_collider.TryGetComponent(out Enemy _health))
            {
                SpawnHitEffect();
                _health.TakeDamage(Player.Instance.damage);
            }
        }
        else if (bulletOwner == BulletOwner.Tower)
        {
            if (_collider.TryGetComponent(out Enemy _health))
            {
                SpawnHitEffect();
                _health.TakeDamage(Tower.Instance._damage);
            }
        }
        else if (bulletOwner == BulletOwner.Enemy)
        {
            if (_collider.TryGetComponent(out Player _health))
            {
                SpawnHitEffect();
                _health.TakeDamage(Player.Instance.damage);
            }
        }

        DestroyProjectile();
    }

    void RangeCheck()
    {
        float currentDistance = Vector3.Distance(startPos, transform.position);

        if (currentDistance > range)
            DestroyProjectile();
    }

    void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    void SpawnHitEffect()
    {
        PoolManager.Instance.SpawnFromPool(hitEffectTag, transform.position, Quaternion.identity);
    }
}

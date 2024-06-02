using UnityEngine;

public class Tower : Health
{
    public static Tower Instance;

    [Header("Tower Settings")]
    [SerializeField] GameObject _towerGO;
    [SerializeField] string _deathEffect;

    [Header("Attack settings")] 
    public int _damage = 100;
    public float _fireRate = 0.05f;
    public GameObject _bulletPrefab;
    public Transform _firePoint;
    public float _projectileSpeed = 20f;
    public float _bulletRange = 5f;

    //[Header("Attack settings")]
    //public float _attackRadius = 20f;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Die()
    {
        //base.Die();
        Player.Instance.TakeDamage(Player.Instance.GetMaxHealthValue());
        _towerGO.SetActive(false);
        PoolManager.Instance.SpawnFromPool(_deathEffect, _towerGO.transform.position, Quaternion.identity);
        GameManager.Instance.EndGame();
    }
}

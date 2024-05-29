using UnityEngine;

public class Tower : Health
{
    public static Tower Instance;

    [Header("Relic settings")] 
    public int _damage = 100;
    public float _fireRate = 0.05f;
    public GameObject _bulletPrefab;
    public Transform _firePoint;
    public float _projectileSpeed = 20f;
    public float _bulletLife = 0.5f;

    [Header("Attack settings")] 
    public float _attackRadius = 20f;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Die()
    {
        Player_Animator.Instance.PlayDeathAnimation();
        base.Die();
        GameManager.Instance.EndGame();
    }
}

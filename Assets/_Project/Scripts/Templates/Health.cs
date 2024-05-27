using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
	[Header("Health Settings")]
	[SerializeField] protected bool isPlayer;
	[SerializeField] protected bool dontDestroy;

	[SerializeField] protected int maxHealth;
	[SerializeField] protected int currentHealth;

	[SerializeField] protected int regenerateAmount;
	[SerializeField] protected float regenerateRate;

	[SerializeField] protected Slider healthBar;

	private float currentRegenTime;
	//private float damageOverTime;

	public virtual bool IsPlayer() { return isPlayer; }
	public virtual int GetMaxHealthValue() { return maxHealth; }
	public virtual int GetCurrentHealthValue() { return currentHealth; }

	public virtual void Heal(int _value)
	{
		currentHealth += _value;
		if (currentHealth >= maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	public virtual void TakeDamage(int _damage)
	{
		currentHealth -= _damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		healthBar.value = 0;

		if (dontDestroy)
		{
			gameObject.SetActive(false);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void RegenerateOverTime()
    {
		currentRegenTime -= Time.deltaTime;

		if(currentRegenTime <= 0)
        {
			Heal(regenerateAmount);
			currentRegenTime = regenerateRate;
        }
    }

	private void ResetHealth()
	{
		currentHealth = maxHealth;

	}

	protected virtual void OnEnable()
	{
		ResetHealth();

		if (healthBar)
		{
			healthBar.minValue = 0;
			healthBar.maxValue = maxHealth;
			healthBar.value = maxHealth;
		}
	}

    protected virtual void LateUpdate()
    {
        if(regenerateRate > 0 && regenerateAmount > 0 && currentHealth < maxHealth)
        {
			RegenerateOverTime();
		}
    }
}

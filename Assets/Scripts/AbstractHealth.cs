using UnityEngine;
using System.Collections;

public abstract class AbstractHealth : MonoBehaviour 
{
	public int MaxHealth = 100;
	public int CriticalModifier = 1;
	
	protected int CurrentHealth;
	protected Animator anim;
	
	void Awake()
	{
		initialize();
	}
	
	protected virtual void initialize()
	{
		CurrentHealth = MaxHealth;
		anim = GetComponent<Animator>();
	}
	
	public virtual void TakeDamage(int damage, bool isCritical, Vector3 hitPoint, Vector3 hitForward, Transform attacker)
	{
		CurrentHealth -= isCritical ? damage * CriticalModifier : damage;
		if(CurrentHealth < 0)
		{
			CurrentHealth = 0;
		}
	}
	
	public bool IsDead()
	{
		return CurrentHealth <= 0;
	}
}

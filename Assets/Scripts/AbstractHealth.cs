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
		CurrentHealth = MaxHealth;
		anim = GetComponent<Animator>();
	}
	
	public virtual void TakeDamage(int damage, bool isCritical)
	{
		CurrentHealth -= isCritical ? damage * CriticalModifier : damage;
	}
}

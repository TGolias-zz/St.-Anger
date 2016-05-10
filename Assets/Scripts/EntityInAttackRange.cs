using UnityEngine;
using System.Collections;

public class EntityInAttackRange : MonoBehaviour 
{
	private SpirakusMovement spirakusMovement;
	private AbstractHealth health;
	
	void Awake()
	{
		spirakusMovement = GetComponentInParent<SpirakusMovement>();
		health = GetComponentInParent<AbstractHealth>();
	}
	
	void OnTriggerStay(Collider other)
	{	
		if(!health.IsDead())
		{
			AbstractHealth otherHealth = other.GetComponent<AbstractHealth>();
			if(otherHealth != null && (spirakusMovement.AttackSameType || health.GetType() != otherHealth.GetType()))
			{
				spirakusMovement.EntityInAttackRange();
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpirakusMovement : AbstractMovement 
{
	public float AttackDistance = 1.2f;
	public bool KnowsPlayerPositionOnStart = false;
	public bool AttackSameType = false;
	
	public bool MoveLocked;
	
	private Animator anim;
	private NavMeshAgent nav;
	private AbstractHealth health;
	
	private Vector3? targetPosition;
	private AbstractHealth targetHealth;

	void Awake()
	{
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		health = GetComponent<AbstractHealth>();
		if(KnowsPlayerPositionOnStart)
		{
			GameObject target = Cache.GetCachedGameObjectByTag(Tags.PLAYER);
			targetPosition = target.transform.position;
			targetHealth = target.GetComponent<AbstractHealth>();
		}
		else
		{
			targetPosition = null;
			targetHealth = null;
		}
		MoveLocked = false;
	}
	
	private List<int> stuffHit;
	
	public void HitTrigger(Collider other, Vector3 hitPosition)
	{
		if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash == AnimationIDs.ATTACK_STATE)
		{
			GameObject otherGameObject = other.gameObject;
			AbstractHealth otherHealth = otherGameObject.GetComponent<AbstractHealth>();
			if(otherHealth != null && !stuffHit.Contains(otherGameObject.GetHashCode()))
			{	
				stuffHit.Add(otherGameObject.GetHashCode());			
				otherHealth.TakeDamage(40, true, hitPosition, transform.forward, transform);
				anim.SetBool(AnimationIDs.HITENEMY, true);
			}
		}
	}
	
	public void EntityIsFound(Vector3 newPosition, AbstractHealth newHealth)
	{
		if((AttackSameType || health.GetType() != newHealth.GetType()) &&
		   (!targetPosition.HasValue || newHealth == targetHealth || 
		   Vector3.Distance(transform.position, newPosition) < Vector3.Distance(transform.position, targetPosition.Value)))
		{
			targetPosition = newPosition;
			targetHealth = newHealth;
		}
	}
	
	public void EntityInAttackRange()
	{
		if(!MoveLocked)
		{
			stuffHit = new List<int>();
			MoveLocked = true;
			anim.SetBool(AnimationIDs.IS_RUNNING, false);
			anim.SetBool(AnimationIDs.HITENEMY, false);
			anim.SetTrigger(AnimationIDs.IS_ATTACKING);
			nav.Stop();
		}
	}
	
	void Update()
	{
		if(!MoveLocked)
		{
			float distance = 0f;
			if(targetPosition.HasValue)
			{
				Vector3 currentPos = transform.position;
				Vector2 currentPos2 = new Vector2(currentPos.x, currentPos.z);
				Vector3 targetPos = targetPosition.Value;
				Vector2 targetPos2 = new Vector2(targetPos.x, targetPos.z);
				distance = Vector2.Distance(currentPos2, targetPos2);
			}
			
			if((targetHealth != null && targetHealth.IsDead()) || distance == 0f)
			{
				isMakingSound = false;
				targetPosition = null;
				targetHealth = null;
				anim.SetBool(AnimationIDs.IS_RUNNING, false);
				nav.Stop();
			}
			else
			{
				isMakingSound = true;
				anim.SetBool(AnimationIDs.IS_RUNNING, true);
				nav.SetDestination(targetPosition.Value);
				nav.Resume();
			}
		}
	}
}

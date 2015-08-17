using UnityEngine;
using System.Collections;

public class SpirakusHealth : AbstractHealth 
{
	public GameObject bloodSpray;
	
	private SpirakusMovement spirakusMovement;
	private NavMeshAgent nav;
	private Transform armature;
	
	protected override void initialize ()
	{
		base.initialize ();
		nav = GetComponent<NavMeshAgent>();
		spirakusMovement = GetComponent<SpirakusMovement>();
		armature = transform.FindChild("Armature");
	}
	
	public override void TakeDamage (int damage, bool isCritical, Vector3 hitPoint, Transform hitFrom)
	{
		if(CurrentHealth > 0)
		{
			base.TakeDamage (damage, isCritical, hitPoint, hitFrom);
			if(CurrentHealth <= 0)
			{
				anim.SetTrigger(AnimationIDs.IS_DEAD);
				spirakusMovement.MoveLocked = true;
				nav.Stop();
				GetComponent<CapsuleCollider>().enabled = false;
			}
			else
			{
				if(isCritical)
				{
					anim.SetTrigger(AnimationIDs.ON_HITINFACE);
					spirakusMovement.MoveLocked = true;
					nav.Stop();
				}
				
				AbstractHealth attackerHealth = hitFrom.GetComponentInParent<AbstractHealth>();
				if(attackerHealth != null)
				{
					spirakusMovement.EntityIsFound(hitFrom.position, attackerHealth);
				}
			}
		}
		GameObject spray = Instantiate(bloodSpray, hitPoint, Quaternion.LookRotation(-hitFrom.forward)) as GameObject;
		spray.transform.parent = armature;
	}
}

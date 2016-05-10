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
	
	public override void TakeDamage (int damage, bool isCritical, Vector3 hitPoint, Vector3 hitForward, Transform attacker)
	{
		if(CurrentHealth > 0)
		{
			base.TakeDamage (damage, isCritical, hitPoint, hitForward, attacker);
			if(CurrentHealth <= 0)
			{
				anim.SetTrigger(AnimationIDs.IS_DEAD);
				spirakusMovement.MoveLocked = true;
				nav.Stop();
				nav.enabled = false;
				spirakusMovement.enabled = false;
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
				
				AbstractHealth attackerHealth = attacker.GetComponent<AbstractHealth>();
				if(attackerHealth != null)
				{
					spirakusMovement.EntityIsFound(attacker.position, attackerHealth);
				}
			}
		}
		GameObject spray = Instantiate(bloodSpray, hitPoint, Quaternion.LookRotation(-hitForward)) as GameObject;
		spray.transform.parent = armature;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerHealth : AbstractHealth 
{
	public GameObject bloodSpray;
	
	private PlayerMovement playerMovement;
	private Transform armature;
	
	protected override void initialize ()
	{
		base.initialize ();
		playerMovement = GetComponent<PlayerMovement>();
		armature = transform.FindChild("Armature");
	}
	
	public override void TakeDamage (int damage, bool isCritical, Vector3 hitPoint, Vector3 hitForward, Transform attacker)
	{
		if(CurrentHealth > 0)
		{
			base.TakeDamage (damage, isCritical, hitPoint, hitForward, attacker);
			if(CurrentHealth <= 0)
			{
				anim.SetTrigger(AnimationIDs.IS_DYING);
				Cache.OnLevelReset();
				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				if(!playerMovement.MoveLocked || playerMovement.ForceAim)
				{
					if(isCritical)
					{
						anim.SetTrigger(AnimationIDs.IS_HITDOWN);
					}
					else
					{
						anim.SetTrigger(AnimationIDs.IS_HIT);
					}
				}
			}
			UIController.Instance.SetHealth(CurrentHealth);
			playerMovement.MoveLocked = true;
		}
		GameObject spray = Instantiate(bloodSpray, hitPoint, Quaternion.LookRotation(-hitForward)) as GameObject;
		spray.transform.parent = armature;
	}
}

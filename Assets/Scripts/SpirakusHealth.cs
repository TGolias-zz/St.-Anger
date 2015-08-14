using UnityEngine;
using System.Collections;

public class SpirakusHealth : AbstractHealth 
{
	public override void TakeDamage (int damage, bool isCritical)
	{
		base.TakeDamage (damage, isCritical);
		if(CurrentHealth <= 0)
		{
			anim.SetTrigger(AnimationIDs.IS_DEAD);
		}
		else if(isCritical)
		{
			anim.SetTrigger(AnimationIDs.ON_HITINFACE);
		}
	}
}

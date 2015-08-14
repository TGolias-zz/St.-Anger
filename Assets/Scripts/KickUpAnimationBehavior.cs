using UnityEngine;
using System.Collections;

public class KickUpAnimationBehavior : StateMachineBehaviour 
{
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		Cache.GetCachedGameObjectByTag(Tags.PLAYER).GetComponent<PlayerMovement>().MoveLocked = false;
	}
}

using UnityEngine;
using System.Collections;

public class RemovePlayerMovelock : StateMachineBehaviour 
{
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if(animator.GetCurrentAnimatorStateInfo(0).fullPathHash == AnimationIDs.IDLE_STATE)
		{
			PlayerMovement playerMovement = animator.gameObject.GetComponent<PlayerMovement>();
			playerMovement.MoveLocked = false;
			playerMovement.ForceAim = false;
		}
	}
}

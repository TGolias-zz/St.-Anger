using UnityEngine;
using System.Collections;

public class AddPlayerMovelock : StateMachineBehaviour 
{
	public bool ForceAim = false;
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		PlayerMovement playerMovement = animator.gameObject.GetComponent<PlayerMovement>();
		playerMovement.MoveLocked = true;
		playerMovement.ForceAim = ForceAim;
	}
}

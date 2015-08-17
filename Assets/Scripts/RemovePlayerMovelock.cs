using UnityEngine;
using System.Collections;

public class RemovePlayerMovelock : StateMachineBehaviour 
{
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		animator.gameObject.GetComponent<PlayerMovement>().MoveLocked = false;
	}
}

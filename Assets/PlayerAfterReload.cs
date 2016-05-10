using UnityEngine;
using System.Collections;

public class PlayerAfterReload : StateMachineBehaviour 
{
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		animator.SetBool(AnimationIDs.IS_RELOADING, false);
	}
}

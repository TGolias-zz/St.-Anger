using UnityEngine;
using System.Collections;

public class RemoveSpirakusMoveLock : StateMachineBehaviour {
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		animator.gameObject.GetComponent<SpirakusMovement>().MoveLocked = false;
	}
}

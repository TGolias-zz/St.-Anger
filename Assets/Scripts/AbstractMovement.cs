using UnityEngine;
using System.Collections;

public class AbstractMovement : MonoBehaviour 
{
	protected bool isMakingSound = false;
	
	public bool IsMakingSound()
	{
		return isMakingSound;
	}
}

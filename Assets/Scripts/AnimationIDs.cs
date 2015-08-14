using UnityEngine;
using System.Collections;

public class AnimationIDs : MonoBehaviour 
{
	public static int IS_DYING;
	public static int IS_WALKING;
	public static int IS_SPRINTING;
	public static int IS_OUTOFSTAMINA;
	public static int IS_ATMAXSTAMINA;
	public static int IS_HOLDINGITEM;
	public static int IS_AIMING;
	public static int FLOAT_HORIZONTAL;
	public static int FLOAT_VERTICAL;
	public static int ON_HITINFACE;
	public static int IS_DEAD;
	public static int GUNISSHOT;
	
	void Awake()
	{
		Cursor.visible = false;
	
		IS_DYING = Animator.StringToHash("IsDying");
		IS_WALKING = Animator.StringToHash("IsWalking");
		IS_SPRINTING = Animator.StringToHash("IsSprinting");
		IS_OUTOFSTAMINA = Animator.StringToHash("IsOutOfStamina");
		IS_ATMAXSTAMINA = Animator.StringToHash("IsAtMaxStamina");
		IS_HOLDINGITEM = Animator.StringToHash("IsHoldingItem");
		IS_AIMING = Animator.StringToHash("IsAiming");
		FLOAT_HORIZONTAL = Animator.StringToHash("Horizontal");
		FLOAT_VERTICAL = Animator.StringToHash("Vertical");
		
		ON_HITINFACE = Animator.StringToHash("OnHitInFace");
		IS_DEAD = Animator.StringToHash("IsDead");
		
		GUNISSHOT = Animator.StringToHash("GunIsShot");
	}
}

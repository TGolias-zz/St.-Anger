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
	public static int IS_HITDOWN;
	public static int IS_HIT;
	public static int FLOAT_HORIZONTAL;
	public static int FLOAT_VERTICAL;
	
	public static int ON_HITINFACE;
	public static int IS_DEAD;
	public static int IS_ATTACKING;
	public static int HITENEMY;
	public static int IS_RUNNING;
	public static int ATTACK_STATE;
	
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
		IS_HITDOWN = Animator.StringToHash("IsHitDown");
		IS_HIT = Animator.StringToHash("IsHit");
		FLOAT_HORIZONTAL = Animator.StringToHash("Horizontal");
		FLOAT_VERTICAL = Animator.StringToHash("Vertical");
		
		ON_HITINFACE = Animator.StringToHash("OnHitInFace");
		IS_DEAD = Animator.StringToHash("IsDead");
		IS_ATTACKING = Animator.StringToHash("IsAttacking");
		HITENEMY = Animator.StringToHash("HitEnemy");
		IS_RUNNING = Animator.StringToHash("IsRunning");
		ATTACK_STATE = Animator.StringToHash("Base Layer.Attack");
		
		GUNISSHOT = Animator.StringToHash("GunIsShot");
	}
}

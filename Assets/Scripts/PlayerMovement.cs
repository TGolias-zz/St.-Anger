using UnityEngine;
using System.Collections;

public class PlayerMovement : AbstractMovement 
{
	public float WalkSpeed = 2.5f;
	public float RunSpeed = 6f;
	public float SpeedDamping = 20f;
	public float RotationDamping = 20f;
	public float MaxStamina = 5f;
	public bool MoveLocked;
	
	private Animator anim;
	private Rigidbody playerRigidbody;
	private PlayerItemUse playerItemUse;
	private Transform cameraTransform;
	private CameraMovement cameraMovement;
	private float Stamina;
	private bool sprintButton;
	private bool isAiming;

	void Awake()
	{
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		playerItemUse = GetComponent<PlayerItemUse>();
		GameObject camera = Cache.GetCachedGameObjectByTag(Tags.CAMERA);
		cameraTransform = camera.transform;
		cameraMovement = camera.GetComponent<CameraMovement>();
		Stamina = MaxStamina;
		MoveLocked = false;
		sprintButton = false;
		isAiming = false;		
	}
	
	void Update()
	{
		bool willAim = Input.GetButton("Aim") && !sprintButton && !MoveLocked;
		if(isAiming != willAim)
		{
			isAiming = willAim;
			cameraMovement.SetAiming(isAiming);
			playerItemUse.SetAiming(isAiming);
		}
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool isMoving = h != 0 || v != 0;
		sprintButton = Input.GetButton("Sprint");
		
		if(sprintButton && isMoving)
		{
			Stamina -= Time.fixedDeltaTime;
			anim.SetBool(AnimationIDs.IS_ATMAXSTAMINA, false);
			if(Stamina <= 0f)
			{
				if(!MoveLocked)
				{
					anim.SetTrigger(AnimationIDs.IS_OUTOFSTAMINA);
					MoveLocked = true;
				}
				Stamina = 0;
			}
		}
		else
		{
			if(Stamina < 5.0f)
			{
				Stamina += Time.fixedDeltaTime;
				if(Stamina >= 5.0f)
				{
					anim.SetBool(AnimationIDs.IS_ATMAXSTAMINA, true);
					Stamina = 5.0f;
				}
			}
		}
		UIController.Instance.SetStamina(Stamina);
		
		isMoving &= !MoveLocked; 
		isMakingSound = isMoving;
		AnimationManagement(h, v, isMoving);
		MovementManagement(h, v, isMoving);
	}
	
	private void AnimationManagement(float h, float v, bool isMoving)
	{
		anim.SetBool(AnimationIDs.IS_WALKING, isMoving && !sprintButton);
		anim.SetBool(AnimationIDs.IS_SPRINTING, isMoving && sprintButton);
		if(isAiming)
		{
			anim.SetFloat(AnimationIDs.FLOAT_VERTICAL, Mathf.Lerp(anim.GetFloat(AnimationIDs.FLOAT_VERTICAL), v, SpeedDamping * Time.fixedDeltaTime));
			anim.SetFloat(AnimationIDs.FLOAT_HORIZONTAL, Mathf.Lerp(anim.GetFloat(AnimationIDs.FLOAT_VERTICAL), h, SpeedDamping * Time.fixedDeltaTime));
		}
		else
		{
			anim.SetFloat(AnimationIDs.FLOAT_VERTICAL, 1f);
			anim.SetFloat(AnimationIDs.FLOAT_HORIZONTAL, 0f);
		}
	}
	
	private void MovementManagement(float h, float v, bool isMoving)
	{
		if(isMoving)
		{
			Vector3 forwardVector = cameraTransform.forward;
			forwardVector.y = 0f;
			forwardVector.Normalize();
			
			Vector3 rightVector = cameraTransform.right;
			rightVector.y = 0f;
			rightVector.Normalize();
			
			Vector3 movementVector = forwardVector * v + rightVector * h;
			movementVector.Normalize();
			
			playerRigidbody.rotation = Quaternion.Lerp(playerRigidbody.rotation, 
													   Quaternion.LookRotation(isAiming ? forwardVector : movementVector), 
			                                           RotationDamping * Time.fixedDeltaTime);
			                                           
			movementVector *= (sprintButton ? RunSpeed : WalkSpeed);
			movementVector.y = playerRigidbody.velocity.y;
			playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, 
			                                        movementVector,
			                                        SpeedDamping * Time.fixedDeltaTime);									  
		}
		else
		{
			if(isAiming)
			{
				Vector3 forwardVector = cameraTransform.forward;
				forwardVector.y = 0f;
				forwardVector.Normalize();
			
				playerRigidbody.rotation = Quaternion.Lerp(playerRigidbody.rotation, 
				                                           Quaternion.LookRotation(forwardVector), 
				                                           RotationDamping * Time.fixedDeltaTime);
			}
			
			Vector3 movementVector = Vector3.zero;
			movementVector.y = playerRigidbody.velocity.y;                                       
			playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, movementVector, SpeedDamping * Time.fixedDeltaTime);
		}
	}
}

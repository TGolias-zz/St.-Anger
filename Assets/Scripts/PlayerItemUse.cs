using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerItemUse : MonoBehaviour 
{
	public float AimDamping = 20.0f;
	
	private int currentItemIndex;
	private ItemScript currentItemScript;

	private Animator anim;
	private Transform cameraTransform;
	private PlayerMovement playerMovement;
	
	private Dictionary<ItemType, GameObject> items;
	
	private Transform upperRightArmTransform;
	private Transform lowerRightArmTransform;
	private Transform rightHandTransform;
	private Transform holdPointTransform;
	private Transform ammoPointTransform;
	private bool isAiming;

	void Awake()
	{
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		cameraTransform = Cache.GetCachedGameObjectByTag(Tags.CAMERA).transform;

		firstAimFrame = true;
		
		anim.SetLayerWeight(1, 1.0f);
		anim.SetLayerWeight(2, 1.0f);
		anim.SetLayerWeight(3, 1.0f);
		
		Transform backUpper = transform.Find("Armature/Back_Lower/Back_Middle/Back_Upper");
		upperRightArmTransform = backUpper.Find("Shoulder_Right/Arm_Upper_Right");
		lowerRightArmTransform = upperRightArmTransform.FindChild("Arm_Lower_Right");
		rightHandTransform = lowerRightArmTransform.FindChild("Hand_Right");
		holdPointTransform = rightHandTransform.FindChild("HoldPoint");
		ammoPointTransform = backUpper.Find ("Shoulder_Left/Arm_Upper_Left/Arm_Lower_Left/Hand_Left/HoldPoint");
		isAiming = false;
		
		items = new Dictionary<ItemType, GameObject>();
		items.Add(ItemType.none, null);
		currentItemIndex = 0;
		currentItemScript = null;
	}
	
	public void AddItem(GameObject newItem)
	{
		ItemScript itemScript = newItem.GetComponent<ItemScript>();
		if(itemScript != null)
		{
			ItemType itemType = itemScript.GetItemType();
			if(items.ContainsKey(itemType))
			{
				ItemScript existing = items[itemType].GetComponent<ItemScript>();
				existing.AddSettingsFrom(itemScript);
				Destroy(newItem);
			}
			else
			{
				newItem.SetActive(false);
				newItem.transform.position = holdPointTransform.position;
				newItem.transform.rotation = holdPointTransform.rotation;
				newItem.transform.parent = holdPointTransform;
				itemScript.Init(transform);
				items.Add (itemScript.GetItemType(), newItem);
			}
		}
	}
	
	void SwitchItem()
	{	
		ItemType key = items.Keys.ElementAt(currentItemIndex);
		GameObject item = items[key];
		int currentlyLoadedAmmo = 0;
		int currentlyUnloadedAmmo = 0;
		
		if(currentItemScript != null)
		{
			ItemType oldKey = currentItemScript.GetItemType();
			if(items.ContainsKey(oldKey))
			{
				GameObject oldGameObject = items[oldKey];
				oldGameObject.SetActive(false);
			}
			currentItemScript = null;
		}
		
		if(item != null)
		{
			item.SetActive(true);
			currentItemScript = item.GetComponent<ItemScript>();
			currentlyLoadedAmmo = currentItemScript.GetCurrentlyLoadedAmmo();
			currentlyUnloadedAmmo = currentItemScript.GetCurrentlyUnloadedAmmo();
			anim.SetBool (AnimationIDs.IS_HOLDINGITEM, true);
		}
		else
		{
			anim.SetBool (AnimationIDs.IS_HOLDINGITEM, false);
		}
		UIController.Instance.ChangeItem(key, currentlyLoadedAmmo, currentlyUnloadedAmmo);
	}

	//Used in LateUpdate when aiming to smoothen the arm movement
	Vector3 currentUpperRightArmRotation;
	Vector3 targetUpperRightArmRotation;
	Vector3 currentUpperRightArmPosition;
	Vector3 targetUpperRightArmPosition;
	Vector3 currentLowerRightArmRotation;
	Vector3 targetLowerRightArmRotation;
	Vector3 currentRightHandRotation;
	Vector3 targetRightHandRotation;

	float targetUpperArmPercentage;
	float currentUpperArmPercentage = 0f;

	private bool firstAimFrame;

	void LateUpdate()
	{
		Quaternion startUpperArmRotation = upperRightArmTransform.rotation;
		Quaternion endUpperArmRotation = Quaternion.LookRotation(cameraTransform.forward);

		//Start of experimentation
		//Vector3 axis = startUpperArmRotation * endUpperArmRotation;

		//End of experimentation

		if(isAiming)
		{
			if(currentItemScript != null)
			{
				switch(currentItemScript.GetItemType())
				{
					case ItemType.pistol:
						targetUpperRightArmRotation = new Vector3(90f, 90f, 90f);
						targetUpperRightArmPosition = new Vector3(0f, 0f, -0.05f);
						targetLowerRightArmRotation = new Vector3(-20f, 0f, 10f);
						targetRightHandRotation = new Vector3(-11.5f, 0f, 15f);
						targetUpperArmPercentage = 1f;
						break;
					case ItemType.crowbar:
						if(IsMeleeAttacking())
						{
							//Upper Arm Rotation
							targetUpperRightArmRotation = new Vector3(90f, 90f, 90f);
							targetUpperArmPercentage = 1f;

						}
						else
						{
							targetUpperRightArmRotation = Vector3.zero; //new Vector3(90f, 90f, 90f);
							targetUpperArmPercentage = 0f;
						}
						break;
					case ItemType.flashlight:
						targetUpperRightArmRotation = new Vector3(90f, 30f, 20f);
						targetUpperRightArmPosition = new Vector3(0f, 0f, -0.05f);
						targetLowerRightArmRotation = new Vector3(-10f, 0f, 10f);
						targetRightHandRotation = new Vector3(0f, 0f, -90f);
						targetUpperArmPercentage = 1f;
						break;
				}
				currentItemScript.UpdateWhenAiming();
			}
			firstAimFrame = false;
		}
		else
		{
			//Set Arm Rotations back to zero
			targetUpperRightArmRotation = Vector3.zero;
			targetUpperRightArmPosition = Vector3.zero;
			targetLowerRightArmRotation = Vector3.zero;
			targetRightHandRotation = Vector3.zero;
			targetUpperArmPercentage = 0f;
			
			if(Input.GetButtonDown("PreviousWeapon"))
			{
				CancelReload();
				currentItemIndex--;
				if(currentItemIndex < 0)
				{
					currentItemIndex = items.Keys.Count - 1;
				}
				SwitchItem();
			}
			if(Input.GetButtonDown("NextWeapon"))
			{
				CancelReload();
				currentItemIndex++;
				if(currentItemIndex >= items.Keys.Count)
				{
					currentItemIndex = 0;
				}
				SwitchItem();
			}
			firstAimFrame = true;
		}

		//Set arm rotations (used when aiming)
		currentUpperArmPercentage = Mathf.Lerp(currentUpperArmPercentage, targetUpperArmPercentage, AimDamping * Time.deltaTime);
		Quaternion nextAngle = Quaternion.LerpUnclamped(startUpperArmRotation, endUpperArmRotation, currentUpperArmPercentage);
		Quaternion midpoint = Quaternion.LerpUnclamped(startUpperArmRotation, endUpperArmRotation, 0.5f);
		//We don't want his shoulder to rotate backwards... That would look weird
		if(midpoint.eulerAngles.x > 180.0f)
		{
			//We want to create a mid point so it will go the long way.
			if(currentUpperArmPercentage < 0.5f)
			{
				nextAngle = Quaternion.LerpUnclamped(startUpperArmRotation, Quaternion.Inverse(midpoint), currentUpperArmPercentage * 2.0f);
			}
			else
			{
				nextAngle = Quaternion.LerpUnclamped(Quaternion.Inverse(midpoint), endUpperArmRotation, (currentUpperArmPercentage - 0.5f) * 2.0f);
			}
		}
		upperRightArmTransform.rotation = nextAngle;

		currentUpperRightArmRotation = Vector3.Lerp(currentUpperRightArmRotation, targetUpperRightArmRotation, AimDamping * Time.deltaTime);
		upperRightArmTransform.Rotate(currentUpperRightArmRotation);
		currentUpperRightArmPosition = Vector3.Lerp(currentUpperRightArmPosition, targetUpperRightArmPosition, AimDamping * Time.deltaTime);
		upperRightArmTransform.Translate(currentUpperRightArmPosition);
		currentLowerRightArmRotation = Vector3.Lerp(currentLowerRightArmRotation, targetLowerRightArmRotation, AimDamping * Time.deltaTime);
		lowerRightArmTransform.Rotate(currentLowerRightArmRotation);
		currentRightHandRotation = Vector3.Lerp(currentRightHandRotation, targetRightHandRotation, AimDamping * Time.deltaTime);
		rightHandTransform.Rotate(currentRightHandRotation);
		
		if(currentItemScript != null)
		{
			if(Input.GetButtonDown("UseItem"))
			{
				if(!currentItemScript.UseItem())
				{
					//Remove the item.
					GameObject oldItem = items[currentItemScript.GetItemType()];
					items.Remove(currentItemScript.GetItemType());
					Destroy(oldItem);
					if(currentItemIndex >= items.Keys.Count)
					{
						currentItemIndex = 0;
					}
					SwitchItem();
				}
			}
		}
	}
	
	public void SetAiming(bool value)
	{
		isAiming = value;
		if(currentItemScript != null)
		{
			currentItemScript.IsAiming(isAiming);
			anim.SetBool(AnimationIDs.IS_AIMING, isAiming && currentItemScript.KeepArmStiff());
			if(isAiming)
			{
				CancelReload();
			}
		}
	}
	
	public void UseMeleeAttack()
	{
		anim.SetTrigger(AnimationIDs.MELEE_ATTACK);
		playerMovement.MoveLocked = true;
		playerMovement.ForceAim = true;
	}
	
	public bool IsMeleeAttacking()
	{
		return anim.GetCurrentAnimatorStateInfo(0).fullPathHash == AnimationIDs.MELEE_STATE;
	}
	
	public void StartReload(GameObject ammoClip)
	{
		if(!isAiming && !playerMovement.MoveLocked && !playerMovement.IsSprinting() 
		   && anim.GetCurrentAnimatorStateInfo(3).fullPathHash != AnimationIDs.RELOADING_STATE)
		{
			GameObject ammoObject = Instantiate(ammoClip, ammoPointTransform.position, ammoPointTransform.rotation) as GameObject;
			ammoObject.transform.parent = ammoPointTransform;
			anim.SetBool(AnimationIDs.IS_RELOADING, true);
		}
	}
	
	public void CancelReload()
	{
		if(anim.GetCurrentAnimatorStateInfo(3).fullPathHash == AnimationIDs.RELOADING_STATE)
		{
			if(ammoPointTransform.childCount > 0)
			{
				anim.SetBool(AnimationIDs.IS_RELOADING, false);
				Destroy(ammoPointTransform.GetChild(0).gameObject);
			}
		}
	}
	
	public void OnReload()
	{
		currentItemScript.OnReload();
		if(ammoPointTransform.childCount > 0)
		{
			Destroy(ammoPointTransform.GetChild(0).gameObject);
		}
	}
	
	public bool IsMoveLocked()
	{
		return playerMovement.MoveLocked;
	}
}

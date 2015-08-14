using UnityEngine;
using System.Collections;

public class PlayerItemUse : MonoBehaviour {

	public GameObject currentItem;
	public ItemScript currentItemScript;

	private Animator anim;
	private Transform cameraTransform;
	
	private Transform upperRightArmTransform;
	private Transform lowerRightArmTransform;
	private Transform rightHandTransform;
	private Transform holdPointTransform;
	private bool isAmining;

	void Awake()
	{
		anim = GetComponent<Animator>();
		cameraTransform = Cache.GetCachedGameObjectByTag(Tags.CAMERA).transform;
		
		anim.SetLayerWeight(1, 1.0f);
		anim.SetLayerWeight(2, 1.0f);
		
		upperRightArmTransform = transform.Find("Armature/Back_Lower/Back_Middle/Back_Upper/Shoulder_Right/Arm_Upper_Right");
		lowerRightArmTransform = upperRightArmTransform.FindChild("Arm_Lower_Right");
		rightHandTransform = lowerRightArmTransform.FindChild("Hand_Right");
		holdPointTransform = rightHandTransform.FindChild("HoldPoint");
		
		isAmining = false;
	}
	
	void Start()
	{
		GameObject newObject = Instantiate(currentItem, holdPointTransform.position, holdPointTransform.rotation) as GameObject;
		newObject.transform.parent = holdPointTransform;
		currentItemScript = newObject.GetComponent<ItemScript>();
		anim.SetBool (AnimationIDs.IS_HOLDINGITEM, true);
	}
	
	void LateUpdate()
	{
		if(isAmining)
		{
			upperRightArmTransform.rotation = Quaternion.LookRotation(cameraTransform.forward);
			upperRightArmTransform.Rotate(90f, 90f, 90f);
			upperRightArmTransform.Translate(0f, 0f, -0.05f);
			lowerRightArmTransform.Rotate(-20f, 0f, 10f);
			rightHandTransform.Rotate(-10f, 0f, 20f);
		}
		
		if(Input.GetButtonDown("UseItem"))
		{
			currentItemScript.UseItem();
		}
	}
	
	public void SetAiming(bool value)
	{
		isAmining = value;
		currentItemScript.IsAiming(isAmining);
		anim.SetBool(AnimationIDs.IS_AIMING, isAmining);
	}
}

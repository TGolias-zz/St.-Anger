using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowbarScript : MonoBehaviour, ItemScript 
{
	public int Damage = 10;
	
	private PlayerItemUse playerInventory;
	private Transform playerTransform;
	
	void Awake()
	{
	}
	
	public void Init(Transform playerTransform)
	{
		this.playerTransform = playerTransform;
		playerInventory = playerTransform.GetComponent<PlayerItemUse>();
	}
			
	public void IsAiming(bool isAiming)
	{
	}
	
	private Ray shootRay;
	private RaycastHit shootHit;
	
	public bool UseItem()
	{
		if(!playerInventory.IsMoveLocked())
		{
			stuffHit = new List<int>();
			playerInventory.UseMeleeAttack();
		}
		return true;
	}
	
	private List<int> stuffHit;
	
	public void HitTrigger(Collider other, Vector3 hitPoint)
	{
		if(playerInventory != null && playerInventory.IsMeleeAttacking())
		{
			GameObject otherGameObject = other.gameObject;
			AbstractHealth otherHealth = otherGameObject.GetComponent<AbstractHealth>();
			if(otherHealth != null && !stuffHit.Contains(otherGameObject.GetHashCode()))
			{	
				stuffHit.Add(otherGameObject.GetHashCode());
				otherHealth.TakeDamage(Damage, other.CompareTag(Tags.WEAKPOINT), hitPoint, transform.forward, playerTransform);
			}
		}
	}
	
	public ItemType GetItemType()
	{
		return ItemType.crowbar;
	}
	
	public int GetCurrentlyLoadedAmmo()
	{
		return 0;
	}
	
	public int GetCurrentlyUnloadedAmmo()
	{
		return 0;
	}
	
	public void AddSettingsFrom(ItemScript itemScript)
	{
	}
	
	public void UpdateWhenAiming()
	{
	}
	
	public void OnReload()
	{
	}

	public bool KeepArmStiff()
	{
		return false;
	}
}

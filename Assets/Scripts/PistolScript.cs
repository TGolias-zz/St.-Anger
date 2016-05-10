using UnityEngine;
using System.Collections;

public class PistolScript : MonoBehaviour, ItemScript 
{
	public int Damage = 10;
	public GameObject AmmoClip;
	public GameObject Gunfire;
	
	public int CurrentlyLoadedAmmo = 0;
	public int CurrentlyUnloadedAmmo = 14;
	private const int AMMOCAPACITY = 6;
	private const int MAXAMMO = 1500;

	private Animator anim;
	private Transform tipOfGun;
	private LineRenderer laserPoint;
	private Transform playerTransform;
	private PlayerItemUse playerInventory;
	
	private bool playerIsAiming;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		tipOfGun = transform.FindChild("LaserPoint");	
		laserPoint = tipOfGun.GetComponent<LineRenderer>();
	}
	
	public void Init(Transform playerTransform)
	{
		this.playerTransform = playerTransform;
		playerInventory = playerTransform.GetComponent<PlayerItemUse>();
	}
	
	public void IsAiming(bool isAiming)
	{
		playerIsAiming = isAiming;
		laserPoint.enabled = playerIsAiming;
	}
	
	public void UpdateWhenAiming()
	{
		Vector3 bulletOrigin = tipOfGun.transform.position;
		laserPoint.SetPosition(0, tipOfGun.InverseTransformPoint(bulletOrigin));
		laserPoint.SetPosition(1, tipOfGun.InverseTransformPoint(bulletOrigin + (tipOfGun.forward * 505f)));
	}
	
	public bool UseItem()
	{
		if(playerIsAiming)
		{
			if(CurrentlyLoadedAmmo > 0)
			{
				CurrentlyLoadedAmmo--;
				UIController.Instance.OnAmmoFired();

				GameObject gunFire = Instantiate(Gunfire, tipOfGun.position, tipOfGun.rotation) as GameObject;
				gunFire.transform.SetParent(tipOfGun);
				
				Ray shootRay = new Ray();
				RaycastHit shootHit;
				shootRay.origin = tipOfGun.position;
				shootRay.direction = tipOfGun.forward;
				if(Physics.Raycast(shootRay, out shootHit, 505f))
				{
					Collider hitCollider = shootHit.collider;
					AbstractHealth health = hitCollider.GetComponentInParent<AbstractHealth>();
					if(health != null)
					{
						health.TakeDamage(Damage, hitCollider.CompareTag(Tags.WEAKPOINT), shootHit.point, shootRay.direction, playerTransform);
					}
				}
			}
			anim.SetTrigger(AnimationIDs.GUNISSHOT);
		}
		else
		{
			if(CurrentlyLoadedAmmo < AMMOCAPACITY && CurrentlyUnloadedAmmo > 0)
			{	
				playerInventory.StartReload(AmmoClip);
			}
		}
		return true;
	}
	
	public ItemType GetItemType()
	{
		return ItemType.pistol;
	}
	
	public int GetCurrentlyLoadedAmmo()
	{
		return CurrentlyLoadedAmmo;
	}
	
	public int GetCurrentlyUnloadedAmmo()
	{
		return CurrentlyUnloadedAmmo;
	}
	
	public void AddSettingsFrom(ItemScript itemScript)
	{
		PistolScript pistolScript = itemScript as PistolScript;
		if(pistolScript != null)
		{
			CurrentlyUnloadedAmmo += pistolScript.CurrentlyUnloadedAmmo + pistolScript.CurrentlyLoadedAmmo;
			if(CurrentlyUnloadedAmmo > MAXAMMO)
			{
				CurrentlyUnloadedAmmo = MAXAMMO;
			}
		}
		UIController.Instance.OnAmmoPickup(ItemType.pistol, CurrentlyUnloadedAmmo);
	}
	
	public void OnReload()
	{
		int loadAmount = Mathf.Min(AMMOCAPACITY - CurrentlyLoadedAmmo, CurrentlyUnloadedAmmo);
		CurrentlyLoadedAmmo += loadAmount;
		CurrentlyUnloadedAmmo -= loadAmount;
		UIController.Instance.OnAmmoLoad(loadAmount);
	}

	public bool KeepArmStiff()
	{
		return true;
	}
}

using UnityEngine;
using System.Collections;

public class PistolScript : MonoBehaviour, ItemScript {

	public int Damage = 10;
	
	private int CurrentlyLoadedAmmo = 0;
	private const int AMMOCAPACITY = 3;
	private int CurrentlyUnloadedAmmo = 10;
	private const int MAXCAPACITY = 24;

	private Animator anim;
	private Transform tipOfGun;
	private LineRenderer laserPoint;
	private ParticleSystem gunFire;
	
	private bool playerIsAiming;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		tipOfGun = transform.FindChild("LaserPoint");
		laserPoint = tipOfGun.GetComponent<LineRenderer>();
		gunFire = tipOfGun.GetComponentInChildren<ParticleSystem>();
	}
	
	public void IsAiming(bool isAiming)
	{
		playerIsAiming = isAiming;
		laserPoint.enabled = playerIsAiming;
	}
	
	private Ray shootRay;
	private RaycastHit shootHit;
	
	public bool UseItem()
	{
		if(playerIsAiming)
		{
			if(CurrentlyLoadedAmmo > 0)
			{
				CurrentlyLoadedAmmo--;
				UIController.Instance.OnAmmoFired();
				gunFire.Stop();
				gunFire.Play();
				
				shootRay.origin = tipOfGun.position;
				shootRay.direction = tipOfGun.forward;
				if(Physics.Raycast(shootRay, out shootHit, 100f))
				{
					Collider hitCollider = shootHit.collider;
					AbstractHealth health = hitCollider.GetComponentInParent<AbstractHealth>();
					if(health != null)
					{
						health.TakeDamage(Damage, hitCollider.CompareTag(Tags.WEAKPOINT), shootHit.point, tipOfGun);
					}
				}
			}
			anim.SetTrigger(AnimationIDs.GUNISSHOT);
		}
		else
		{
			// Reload the gun... This will be written later.
			// WE NEED TO DO A LOAD THE GUN ANIMATION HERE.
			if(CurrentlyLoadedAmmo < AMMOCAPACITY && CurrentlyUnloadedAmmo > 0)
			{
				int loadAmount = Mathf.Min(AMMOCAPACITY - CurrentlyLoadedAmmo, CurrentlyUnloadedAmmo);
				CurrentlyLoadedAmmo += loadAmount;
				CurrentlyUnloadedAmmo -= loadAmount;
				UIController.Instance.OnAmmoLoad(loadAmount);
			}
		}
		return true;
	} 
}

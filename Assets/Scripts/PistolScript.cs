using UnityEngine;
using System.Collections;

public class PistolScript : MonoBehaviour, ItemScript {

	public int Damage = 10;

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
			gunFire.Stop();
			gunFire.Play();
			anim.SetTrigger(AnimationIDs.GUNISSHOT);
			shootRay.origin = tipOfGun.position;
			shootRay.direction = tipOfGun.forward;
			if(Physics.Raycast(shootRay, out shootHit, 100f))
			{
				Collider hitCollider = shootHit.collider;
				AbstractHealth health = hitCollider.GetComponentInParent<AbstractHealth>();
				if(health != null)
				{
					health.TakeDamage(Damage, hitCollider.CompareTag(Tags.WEAKPOINT));
				}
			}
		}
		else
		{
			//Reload the gun... This will be written later.
		}
		return true;
	} 
}

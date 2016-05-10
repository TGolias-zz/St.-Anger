using UnityEngine;
using System.Collections;

public class SpirakusArmScript : MonoBehaviour {

	private SpirakusMovement spirakusMovement;
	private AbstractHealth health;

	void Awake()
	{
		spirakusMovement = GetComponentInParent<SpirakusMovement>();
		health = GetComponentInParent<AbstractHealth>();
	}
	
	void OnTriggerStay(Collider other)
	{
		if(!health.IsDead())
		{
			Ray ray = new Ray(transform.position, other.transform.position - transform.position);
			RaycastHit raycastHit;
			if(Physics.Raycast(ray, out raycastHit, 1f))
			{
				spirakusMovement.HitTrigger(other, raycastHit.point);
			}
		}
	}
}

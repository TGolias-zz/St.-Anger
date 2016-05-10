using UnityEngine;
using System.Collections;

public class EntitySeen : MonoBehaviour 
{
	private SpirakusMovement spirakusMovement;
	
	void Awake()
	{
		spirakusMovement = GetComponentInParent<SpirakusMovement>();
	}
	
	private Ray ray;
	private RaycastHit rayCastHit;
	
	void OnTriggerStay(Collider other)
	{
		AbstractHealth health = other.GetComponent<AbstractHealth>();
		if(health != null)
		{
			ray.origin = transform.position;
			ray.direction = other.transform.position - transform.position;
			if(Physics.Raycast(ray, out rayCastHit, 14f) && rayCastHit.transform == other.transform)
			{
				spirakusMovement.EntityIsFound(other.transform.position, health);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class EntityIsHeard : MonoBehaviour {

	private SpirakusMovement spirakusMovement;
	
	void Awake()
	{
		spirakusMovement = GetComponentInParent<SpirakusMovement>();
	}
	
	void OnTriggerStay(Collider other)
	{
		AbstractHealth health = other.GetComponent<AbstractHealth>();
		AbstractMovement movement = other.GetComponent<AbstractMovement>();
		if(health != null && movement != null && movement.IsMakingSound())
		{
			spirakusMovement.EntityIsFound(other.transform.position, health);
		}
	}
}

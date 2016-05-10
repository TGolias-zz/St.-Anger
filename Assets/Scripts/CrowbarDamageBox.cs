using UnityEngine;
using System.Collections;

public class CrowbarDamageBox : MonoBehaviour 
{
	private CrowbarScript crowbar;
	
	void Awake()
	{
		crowbar = GetComponentInParent<CrowbarScript>();
	}
	
	void OnTriggerStay(Collider other)
	{
		Ray ray = new Ray(transform.position, other.transform.position - transform.position);
		RaycastHit raycastHit;
		if(Physics.Raycast(ray, out raycastHit, 1f))
		{
			crowbar.HitTrigger(other, raycastHit.point);
		}
	}
}

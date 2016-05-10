using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour 
{
	public float rotateSpeed = 180f;
	
	private Vector3 rotation;
	
	// Use this for initialization
	void Awake () 
	{
		rotation = new Vector3(0f, rotateSpeed, 0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(rotation * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		PlayerItemUse playerItems = other.GetComponent<PlayerItemUse>();
		if(playerItems != null)
		{
			Transform item = transform.GetChild(0).GetChild(0);
			playerItems.AddItem(item.gameObject);
			Destroy (gameObject);
		}
	}
}

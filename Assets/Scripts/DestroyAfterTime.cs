using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float LifeTime = 1.0f;
	
	private float timeElapsed;
	
	void Awake () 
	{
		timeElapsed = 0f;
	}
	
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= LifeTime)
		{
			Destroy(gameObject);
		}
	}
}

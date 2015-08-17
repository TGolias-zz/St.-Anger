using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {

	public RawImage[] ammo;
	
	private int currentAmmo = 0;

	void Awake() 
	{
		for(int i = 0; i < ammo.Length; i++)
		{
			ammo[i].enabled = false;
		}
	}
	
	public void LoadAmmo(int amount)
	{
		currentAmmo += amount;
		for(int i = 0; i < ammo.Length; i++)
		{
			ammo[i].enabled = i < currentAmmo;
		}
	}
	
	public void OnBulletFired()
	{
		if(currentAmmo > 0)
		{
			currentAmmo--;
			ammo[currentAmmo].enabled = false;
		}
	}
}

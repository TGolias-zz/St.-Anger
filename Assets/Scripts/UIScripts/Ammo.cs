using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class UnloadedAmmo
{
	public GameObject AmmoImage; //Prefab for ammo
	//Starting point
	public float StartingX;
	public float StartingY;
	//Spacing between bullets
	public float HeightSpace; 
	public float WidthSpace;
	//Max Bullets On Screen
	public int MaxObjectsHigh;
	public int MaxObjectsWide;

}

public class Ammo : MonoBehaviour {

	public Transform ammoParent;
	public Text remainingAmmo;
	public RawImage[] ammo;
	public UnloadedAmmo unloaded;

	private RawImage[] unloadedImages;
	private int currentAmmoCount = 0;
	private int unloadedAmmoCount = 0;

	void Awake() 
	{
		//Instantiate bullet images
		unloadedImages = new RawImage[unloaded.MaxObjectsHigh * unloaded.MaxObjectsWide];
		for(int i = 0; i < unloadedImages.Length; i++)
		{
			GameObject bullet = Instantiate
			(
					unloaded.AmmoImage, 
				new Vector3
				(
					unloaded.StartingX + (unloaded.WidthSpace * (i / unloaded.MaxObjectsHigh)),
					unloaded.StartingY + (unloaded.HeightSpace * (i % unloaded.MaxObjectsHigh)),
					0
				),
				Quaternion.identity
			) as GameObject;
			bullet.transform.SetParent(ammoParent);
			bullet.transform.localScale = new Vector3(1, 1, 1);
			unloadedImages[i] = bullet.GetComponent<RawImage>();
		}
		UnloadAmmo();
	}
	
	public void SetUnloadedAmmo(int amount)
	{
		remainingAmmo.enabled = true;
		if(amount > unloadedAmmoCount)
		{
			int count = amount - unloadedAmmoCount;
			for(int i = 0; i < count; i++)
			{
				unloadedImages[unloadedAmmoCount + i].enabled = true;
			}
		}
		else
		{
			int count = unloadedAmmoCount - amount;
			int index = amount - 1;
			for(int i = 0; i < count; i++)
			{
				unloadedImages[index - i].enabled = false;
			}
		}
		unloadedAmmoCount = amount;
	}
	
	public void SetLoadedAmmo(int amount)
	{
		currentAmmoCount += amount;
		for(int i = 0; i < ammo.Length; i++)
		{
			ammo[i].enabled = i < currentAmmoCount;
		}
	}
	
	public void UnloadAmmo()
	{
		currentAmmoCount = 0;
		for(int i = 0; i < ammo.Length; i++)
		{
			ammo[i].enabled = false;
		}
		for(int i = 0; i < unloadedImages.Length; i++)
		{
			unloadedImages[i].enabled = false;
		}

		remainingAmmo.enabled = false;
	}
	
	public void LoadAmmo(int amount)
	{
		SetLoadedAmmo(amount);
		for(int i = 0; i < amount; i++)
		{
			unloadedAmmoCount--;
			unloadedImages[unloadedAmmoCount].enabled = false;
		}
	}
	
	public void OnBulletFired()
	{
		if(currentAmmoCount > 0)
		{
			currentAmmoCount--;
			ammo[currentAmmoCount].enabled = false;
		}
	}
}

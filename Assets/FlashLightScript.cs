using UnityEngine;
using System.Collections;

public class FlashLightScript : MonoBehaviour, ItemScript
{
	private Light spotLight;
	private Light headLight;

	void Awake()
	{
		Transform flashLightModel = transform.FindChild("FlashlightModel");
		Transform spotlightPoint = flashLightModel.FindChild("SpotlightPoint");
		spotLight = spotlightPoint.GetComponent<Light>();
		Transform headlightPoint = flashLightModel.FindChild("Headlight");
		headLight = headlightPoint.GetComponent<Light>();

	}

	public void Init(Transform playerTransform)
	{
	}

	public void IsAiming(bool isAiming)
	{
	}

	public bool UseItem()
	{
		bool newValue = !spotLight.enabled;
		spotLight.enabled = newValue;
		headLight.enabled = newValue;
		return true;
	}

	public ItemType GetItemType()
	{
		return ItemType.flashlight;
	}

	public int GetCurrentlyLoadedAmmo()
	{
		return 0;
	}
	
	public int GetCurrentlyUnloadedAmmo()
	{
		return 0;
	}
	
	public void AddSettingsFrom(ItemScript itemScript)
	{
	}
	
	public void UpdateWhenAiming()
	{
	}
	
	public void OnReload()
	{
	}

	public bool KeepArmStiff()
	{
		return true;
	}
}

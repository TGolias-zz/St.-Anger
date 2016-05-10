using UnityEngine;
using System.Collections;

public enum ItemType
{
	none,
	pistol,
	crowbar,
	flashlight
}

public interface ItemScript
{
	void Init(Transform playerTransform);
	//Returns false if the item is destroyed after use.
	bool UseItem();
	void IsAiming(bool isAiming);
	void UpdateWhenAiming();
	ItemType GetItemType();
	int GetCurrentlyLoadedAmmo();
	int GetCurrentlyUnloadedAmmo();
	void AddSettingsFrom(ItemScript script);
	void OnReload();
	bool KeepArmStiff();
}
using UnityEngine;
using System.Collections;

public enum ItemType
{
	none,
	gun
}

public interface ItemScript
{
	//Returns false if the item is destroyed after use.
	bool UseItem();
	void IsAiming(bool isAiming);
}
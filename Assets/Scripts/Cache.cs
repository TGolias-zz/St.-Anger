using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Cache 
{
	private static Dictionary<string, GameObject> cachedItems = new Dictionary<string, GameObject>();
	
	public static GameObject GetCachedGameObjectByTag(string tag)
	{
		if(cachedItems.ContainsKey(tag))
		{
			return cachedItems[tag];
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag(tag);
		cachedItems.Add(tag, gameObject);
		return gameObject;
	}
	
	/// <summary>
	/// Make sure this is called before the level is reset to clear the old cached GameObjects
	/// </summary>
	public static void OnLevelReset()
	{
		cachedItems.Clear();
	}
}

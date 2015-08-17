using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
	public float MaxAmount = 100f;
	
	private Scrollbar scrollBar;
	
	void Awake()
	{
		scrollBar = GetComponent<Scrollbar>();
	}
	
	public void OnAmountChanged(float newValue)
	{
		scrollBar.size = newValue / MaxAmount;
	}
}
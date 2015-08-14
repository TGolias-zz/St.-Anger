using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
	
	public Scrollbar healthBar;
	public static int health = 100;
	
	public void Damage(int value)
	{
		health -= value;
		
	}
	
	void Update () {
		if (Input.GetKeyDown ("b")) {
			Damage (10);
		}
		healthBar.size = (float) health / 100.0f;
	}
}
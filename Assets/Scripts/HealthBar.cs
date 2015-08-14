using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar {
	
	public Scrollbar healthBar;
	public static int health = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	public void Damage(int value)
	{
		health -= value;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("b")) {
			Damage (10);
		}
		healthBar.size = (float) health / 100.0f;
	}
}
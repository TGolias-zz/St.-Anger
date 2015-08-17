using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
	public static UIController Instance;
	
	private Bar healthBar;
	private Bar staminaBar;
	private Ammo pistolAmmo;

	void Awake()
	{
		Instance = this;
		healthBar = transform.FindChild("Health Bar").GetComponent<Bar>();
		staminaBar = transform.FindChild("Stamina Bar").GetComponent<Bar>();
		pistolAmmo = transform.FindChild("PistolAmmo").GetComponent<Ammo>();
	}
	
	public void SetHealth(float value)
	{
		healthBar.OnAmountChanged(value);
	}
	
	public void SetStamina(float value)
	{
		staminaBar.OnAmountChanged(value);
	}
	
	public void OnAmmoLoad(int amount)
	{
		pistolAmmo.LoadAmmo(amount);
	}
	
	public void OnAmmoFired()
	{
		pistolAmmo.OnBulletFired();
	}
}

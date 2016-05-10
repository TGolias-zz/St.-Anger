using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
	public static UIController Instance;

	private Bar healthBar;
	private Bar staminaBar;
	private Ammo pistolAmmo;
	private Dictionary<ItemType, UIComponents> items;
	private UIComponents currentComponents;
	private ItemType currentItemtype;
	
	struct UIComponents
	{
		public RawImage ItemImage;
		public Text AmmoText;
		public Ammo AmmoScript;	
	}

	void Awake()
	{
		Instance = this;
		healthBar = transform.FindChild("Health Bar").GetComponent<Bar>();
		staminaBar = transform.FindChild("Stamina Bar").GetComponent<Bar>();
		
		items = new Dictionary<ItemType, UIComponents>();
		
		UIComponents noneComponents = new UIComponents();
		noneComponents.ItemImage = null;
		noneComponents.AmmoText = null;
		noneComponents.AmmoScript = null;
		items.Add(ItemType.none, noneComponents);
		
		UIComponents pistolComponents = new UIComponents();
		pistolComponents.ItemImage = transform.FindChild("PistolImage").GetComponent<RawImage>();
		Transform pistolAmmo = transform.FindChild("PistolAmmo");
		pistolComponents.AmmoText = pistolAmmo.GetComponent<Text>();
		pistolComponents.AmmoScript = pistolAmmo.GetComponent<Ammo>();
		items.Add(ItemType.pistol, pistolComponents);
		
		UIComponents crowbarComponents = new UIComponents();
		crowbarComponents.ItemImage = transform.FindChild("CrowbarImage").GetComponent<RawImage>();
		crowbarComponents.AmmoText = null;
		crowbarComponents.AmmoScript = null;
		items.Add (ItemType.crowbar, crowbarComponents);

		UIComponents flashlightComponents = new UIComponents();
		flashlightComponents.ItemImage = transform.FindChild("FlashlightImage").GetComponent<RawImage>();
		crowbarComponents.AmmoText = null;
		crowbarComponents.AmmoScript = null;
		items.Add (ItemType.flashlight, flashlightComponents);

		currentItemtype = ItemType.none;
		currentComponents = items[currentItemtype];
	}

	public ItemType GetCurrentItemType()
	{
		return currentItemtype;
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
		currentComponents.AmmoScript.LoadAmmo(amount);
	}
	
	public void OnAmmoFired()
	{
		currentComponents.AmmoScript.OnBulletFired();
	}

	public void OnAmmoPickup(ItemType type, int currentlyUnloadedAmmo)
	{
		items[type].AmmoScript.SetUnloadedAmmo(currentlyUnloadedAmmo);
	}
	
	public void ChangeItem(ItemType newItemType, int currentAmmo, int remainingAmmo)
	{
		ShowCurrentItems(false);
		currentItemtype = newItemType;
		currentComponents = items[currentItemtype];
		ShowCurrentItems(true, currentAmmo, remainingAmmo);
	}
	
	private void ShowCurrentItems(bool show, int currentAmmo = 0, int remainingAmmo = 0)
	{
		RawImage rawImage = currentComponents.ItemImage;
		if(rawImage != null)
		{
			rawImage.enabled = show;
		}
		Text ammoText = currentComponents.AmmoText;
		if(ammoText != null)
		{
			ammoText.gameObject.SetActive(show);
		}
		Ammo ammoScript = currentComponents.AmmoScript;
		if(ammoScript != null)
		{
			if(show)
			{
				ammoScript.SetUnloadedAmmo(remainingAmmo);
				ammoScript.SetLoadedAmmo(currentAmmo);
			}
		}
	}
}

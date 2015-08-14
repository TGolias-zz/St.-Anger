using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PistolAmmo : MonoBehaviour {

	public static int ammoCount;
	public static bool onPistol;
	public RawImage ammo1;
	public RawImage ammo2;
	public RawImage ammo3;
	public RawImage ammo4;
	public RawImage ammo5;
	public RawImage ammo6;
	public RawImage ammo7;
	public RawImage ammo8;
	public RawImage ammo9;
	public RawImage ammo10;

	// Use this for initialization
	void Start () {
		ammoCount = 10;
		ammo1.enabled = false;
		ammo2.enabled = false;
		ammo3.enabled = false;
		ammo4.enabled = false;
		ammo5.enabled = false;
		ammo6.enabled = false;
		ammo7.enabled = false;
		ammo8.enabled = false;
		ammo9.enabled = false;
		ammo10.enabled = false;
		onPistol = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G) && onPistol) {
			if (ammoCount == 10) {
				ammoCount--;
				ammo1.enabled = false;
			} else if (ammoCount == 9) {
				ammoCount--;
				ammo2.enabled = false;
			} else if (ammoCount == 8) {
				ammoCount--;
				ammo3.enabled = false;
			} else if (ammoCount == 7) {
				ammoCount--;
				ammo4.enabled = false;
			} else if (ammoCount == 6) {
				ammoCount--;
				ammo5.enabled = false;
			} else if (ammoCount == 5) {
				ammoCount--;
				ammo6.enabled = false;
			} else if (ammoCount == 4) {
				ammoCount--;
				ammo7.enabled = false;
			} else if (ammoCount == 3) {
				ammoCount--;
				ammo8.enabled = false;
			} else if (ammoCount == 2) {
				ammoCount--;
				ammo9.enabled = false;
			} else if (ammoCount == 1) {
				ammoCount--;
				ammo10.enabled = false;
			} else {
				ammoCount = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.R) && onPistol) {
			ammo1.enabled = true;
			ammo2.enabled = true;
			ammo3.enabled = true;
			ammo4.enabled = true;
			ammo5.enabled = true;
			ammo6.enabled = true;
			ammo7.enabled = true;
			ammo8.enabled = true;
			ammo9.enabled = true;
			ammo10.enabled = true;
			ammoCount = 10;
		}
		if (CrowbarIcon.pressCounter % 2 == 0 && Input.GetKeyDown(KeyCode.V)) {
			onPistol = true;
			CrowbarAmmo.onCrowbar = false;
			if (ammoCount == 10) {
				ammo1.enabled = true;
				ammo2.enabled = true;
				ammo3.enabled = true;
				ammo4.enabled = true;
				ammo5.enabled = true;
				ammo6.enabled = true;
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} 
			else if (ammoCount == 9) {
				ammo2.enabled = true;
				ammo3.enabled = true;
				ammo4.enabled = true;
				ammo5.enabled = true;
				ammo6.enabled = true;
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} 
			else if (ammoCount == 8) {
				ammo3.enabled = true;
				ammo4.enabled = true;
				ammo5.enabled = true;
				ammo6.enabled = true;
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} 
			else if (ammoCount == 7) {
				ammo4.enabled = true;
				ammo5.enabled = true;
				ammo6.enabled = true;
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} 
			else if (ammoCount == 6) {
				ammo5.enabled = true;
				ammo6.enabled = true;
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} 
			else if (ammoCount == 5) {
				ammo6.enabled = true;
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} else if (ammoCount == 4) {
				ammo7.enabled = true;
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} else if (ammoCount == 3) {
				ammo8.enabled = true;
				ammo9.enabled = true;
				ammo10.enabled = true;
			} else if (ammoCount == 2) {
				ammo9.enabled = true;
				ammo10.enabled = true;
			} else if (ammoCount == 1) {
				ammo10.enabled = true;
			} else if (ammoCount == 0) {
			}
		}
		if (CrowbarIcon.pressCounter % 2 != 0 && Input.GetKeyDown(KeyCode.V)) {
			onPistol = false;
			CrowbarAmmo.onCrowbar = true;
			ammo1.enabled = false;
			ammo2.enabled = false;
			ammo3.enabled = false;
			ammo4.enabled = false;
			ammo5.enabled = false;
			ammo6.enabled = false;
			ammo7.enabled = false;
			ammo8.enabled = false;
			ammo9.enabled = false;
			ammo10.enabled = false;
		}
	}
}

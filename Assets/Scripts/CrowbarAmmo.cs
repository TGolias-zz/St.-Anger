using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrowbarAmmo : MonoBehaviour {

	public int crowbarAmmoCount;
	public static bool onCrowbar;
	public RawImage ammo1;
	public RawImage ammo2;
	public RawImage ammo3;
	public RawImage ammo4;
	public RawImage ammo5;


	// Use this for initialization
	void Start () {
		onCrowbar = true;
		crowbarAmmoCount = 5;
		ammo1.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G) && onCrowbar) {
			if (crowbarAmmoCount == 5) {
				crowbarAmmoCount--;
				ammo1.enabled = false;
			} else if (crowbarAmmoCount == 4) {
				crowbarAmmoCount--;
				ammo2.enabled = false;
			} else if (crowbarAmmoCount == 3) {
				crowbarAmmoCount--;
				ammo3.enabled = false;
			} else if (crowbarAmmoCount == 2) {
				crowbarAmmoCount--;
				ammo4.enabled = false;
			} else if (crowbarAmmoCount == 1) {
				crowbarAmmoCount--;
				ammo5.enabled = false;
			} else {
				crowbarAmmoCount = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.R) && onCrowbar) {
			ammo1.enabled = true;
			ammo2.enabled = true;
			ammo3.enabled = true;
			ammo4.enabled = true;
			ammo5.enabled = true;
			crowbarAmmoCount = 5;
		}
		if (CrowbarIcon.pressCounter % 2 != 0 && Input.GetKeyDown(KeyCode.V)) {
			onCrowbar = true;
			PistolAmmo.onPistol = false;
			if (crowbarAmmoCount == 5) {
				ammo1.enabled = true;
				ammo2.enabled = true;
				ammo3.enabled = true;
				ammo4.enabled = true;
				ammo5.enabled = true;
			} else if (crowbarAmmoCount == 4) {
				ammo2.enabled = true;
				ammo3.enabled = true;
				ammo4.enabled = true;
				ammo5.enabled = true;
			} else if (crowbarAmmoCount == 3) {
				ammo3.enabled = true;
				ammo4.enabled = true;
				ammo5.enabled = true;
			} else if (crowbarAmmoCount == 2) {
				ammo4.enabled = true;
				ammo5.enabled = true;
			} else if (crowbarAmmoCount == 1) {
				ammo5.enabled = true;
			} else if (crowbarAmmoCount == 0) {
			}
		}
		if (CrowbarIcon.pressCounter % 2 == 0 && Input.GetKeyDown(KeyCode.V)) {
			onCrowbar = false;
			PistolAmmo.onPistol = true;
			ammo1.enabled = false;
			ammo2.enabled = false;
			ammo3.enabled = false;
			ammo4.enabled = false;
			ammo5.enabled = false;
		}
	}
}

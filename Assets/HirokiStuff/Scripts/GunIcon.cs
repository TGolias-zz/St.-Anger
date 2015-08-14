using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunIcon : MonoBehaviour {
	
	public RawImage myText;
	
	// Use this for initialization
	void Start () {
		myText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			Debug.Log("press counter:" + CrowbarIcon.pressCounter);
			if (CrowbarIcon.pressCounter % 2 != 0) {
				myText.enabled = false;
			} else {
				myText.enabled = true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrowbarIcon : MonoBehaviour {
	
	public RawImage myText;
	public static int pressCounter = 2;
	
	// Use this for initialization
	void Start () {
		myText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			pressCounter++;
			Debug.Log("press counter:" + pressCounter);
			if (pressCounter % 2 != 0) {
				myText.enabled = false;
			} else {
				myText.enabled = true;
			}
		}
	}
}
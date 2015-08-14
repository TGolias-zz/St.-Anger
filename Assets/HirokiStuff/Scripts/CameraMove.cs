using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	//float horizSpeed = 10.0f;
	float vertSpeed = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//float h = horizSpeed * Input.GetAxis ("Mouse X");
		if (Input.GetKey ("a")) {
			transform.Rotate(0.0f, 1.0f, 0.0f);
		}
		if (Input.GetKey ("f")) {
			transform.Rotate(0.0f, -1.0f, 0.0f);
		}
		if (Input.GetKey ("up")) {
			transform.Translate (0.0f, 0.0f, 1.0f);
		} 
		if (Input.GetKey ("left")) {
			transform.Translate (-1.0f, 0.0f, 0.0f);
		} 
		if (Input.GetKey ("down")) {
			transform.Translate (0.0f, 0.0f, -1.0f);
		} 
		if (Input.GetKey ("right")) {
			transform.Translate (1.0f, 0.0f, 0.0f);
		}
	}
}

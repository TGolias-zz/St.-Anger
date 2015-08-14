using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public float AimSensitivty = 4f;
	public float AimDistance = 1f;
	public float NormalSensitivity = 8f;
	public float NormalDistance = 3f;
	public float Damping = 20f;
	
	private Transform playerTransform;
	private float currentSensitivity;
	private float currentDistance;
	private float targetSensitivity;
	private float targetDistance;
	private Vector3 currentHeight;
	private bool isAiming;
	
	void Awake()
	{
		playerTransform = Cache.GetCachedGameObjectByTag(Tags.PLAYER).transform;
		isAiming = false;
		currentSensitivity = NormalSensitivity;
		targetSensitivity = NormalSensitivity;
		currentDistance = NormalDistance;
		targetDistance = NormalDistance;
		currentHeight = normalHeight;
	}
	
	private static readonly Vector3 aimHeight = new Vector3(0f, 1.5f, 0f);
	private static readonly Vector3 normalHeight = new Vector3(0f, 1f, 0f);
	
	void LateUpdate () 
	{
		SmoothCamera();
	
		float horizontal = Input.GetAxis("Mouse X") * currentSensitivity;
		float vertical = -Input.GetAxis("Mouse Y") * currentSensitivity;
		Vector3 eulerAngles = transform.eulerAngles;
		float rotX = clampCamera(eulerAngles.x + vertical, 345, 65, 180);
		float rotY = eulerAngles.y + horizontal;
		
		transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
		transform.position = playerTransform.position + currentHeight 
							 - (transform.forward * currentDistance) 
							 + (isAiming ? (playerTransform.right * 0.35f) : Vector3.zero);
	}
	
	private void SmoothCamera()
	{
		currentSensitivity = Mathf.Lerp(currentSensitivity, targetSensitivity, Damping * Time.deltaTime);
		currentDistance = Mathf.Lerp(currentDistance, targetDistance, Damping * Time.deltaTime);
	}
	
	private float clampCamera(float value, int min, int max, int neverHit)
	{
		if(value >= 0 && value < neverHit)
		{
			if(value > max)
			{
				return max;
			}
		}
		else
		{
			if(value < 0)
			{
				if(value + 360 < min)
				{
					return min;
				}
			}
			else if(value < min)
			{
				return min;
			}
		}
		return value;
	}
	
	public void SetAiming(bool value)
	{
		isAiming = value;
		if(isAiming)
		{
			targetSensitivity = AimSensitivty;
			targetDistance = AimDistance;
			currentHeight = aimHeight;
		}
		else
		{
			targetSensitivity = NormalSensitivity;
			targetDistance = NormalDistance;
			currentHeight = normalHeight;
		}
	}
}

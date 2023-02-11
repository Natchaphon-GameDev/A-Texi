using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	[Range(1, 10)]
	public float followSpeed = 2;
	[Range(1, 10)]
	public float lookSpeed = 5;
	public Vector3 initialCameraPosition;

	private void Start()
	{
		GameManager.instance.OnMenu += SetMenuCam;
		GameManager.instance.OnRestart += SetPlayerCam;
	}

	private void LateUpdate()
	{
		//Look at car
		Vector3 _lookDirection = (new Vector3(target.position.x, target.position.y, target.position.z)) - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

		//Move to car
		Vector3 _targetPos = initialCameraPosition + target.transform.position;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}

	private void SetMenuCam()
	{
		enabled = true;
		initialCameraPosition = GameManager.instance.menuCam;
	}
	
	private void SetPlayerCam()
	{
		enabled = false;
		initialCameraPosition = GameManager.instance.playerCam;
	}

}

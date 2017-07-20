using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	Vector3 PosOffset;
	Quaternion RotOffset;

	public cakeslice.OutlineEffect OutlineEffect;

	[HideInInspector]
	public Transform target = null;

	void Start()
	{
		PosOffset = transform.localPosition;
		RotOffset = transform.localRotation;
	}

	void Update()
	{
		Vector3 currentPos = transform.position;
		Quaternion currentRot = transform.rotation;

		if (target != null)
		{
			transform.position = target.transform.position;
		}
		else if (GameManager.Instance.Player != null)
		{
			transform.position = GameManager.Instance.Player.transform.position;
		}

		transform.position += PosOffset;
		transform.rotation = transform.localRotation;

		Vector3 desiredPos = transform.position;
		Quaternion desiredRot = transform.rotation;

		transform.position = Vector3.Lerp(currentPos, desiredPos, Time.deltaTime);
		transform.rotation = Quaternion.Lerp(currentRot, desiredRot, Time.deltaTime);

	}


}

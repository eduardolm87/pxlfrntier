using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	Vector3 PosOffset;
	Quaternion RotOffset;

	public cakeslice.OutlineEffect OutlineEffect;

	void Start()
	{
		PosOffset = transform.localPosition;
		RotOffset = transform.localRotation;
	}

	void Update()
	{
		if (GameManager.Instance.Player == null)
			return;

		//transform.position = new Vector3(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y, transform.position.z);


		transform.position = GameManager.Instance.Player.transform.position;
		transform.position += PosOffset;
		transform.rotation = transform.localRotation;



	}
}

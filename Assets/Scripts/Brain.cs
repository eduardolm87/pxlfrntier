using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
	[HideInInspector]
	public Character character;

	[HideInInspector]
	public Vector3 TargetPosition = Vector3.zero;
	[HideInInspector]
	public bool TargetPositionFollow = false;


	void Awake()
	{
		character = GetComponent<Character>();
	}

	public virtual void Start()
	{

	}
		
	public virtual void Tick()
	{
		if (TargetPositionFollow)
		{
			WalkTowardsTarget();
		}
	}



	public void MoveInDirection(Vector3 Direction)
	{
		character.rigidbody.velocity = Direction;
	}

	public void Stop()
	{
		character.rigidbody.velocity = Vector3.zero;
	}

	public void SetMovementTarget(Vector3 Target)
	{
		TargetPosition = Target;
		TargetPositionFollow = true;

		//Debug.Log("Target " + Target);
	}



	const float MinDistanceToStopWalking = 0.01f;

	void WalkTowardsTarget()
	{
		float Distance = Vector3.SqrMagnitude(TargetPosition - transform.position);
		if (Distance > MinDistanceToStopWalking)
		{
			Vector3 Movement = TargetPosition - transform.position;
			Movement = Movement.normalized * character.stats.Speed;

			MoveInDirection(Movement);
		}
		else
		{
			Stop();
			TargetPositionFollow = false;
		}
	}
}

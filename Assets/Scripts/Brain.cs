using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [HideInInspector]
    public Character character;


    void Awake()
    {
        character = GetComponent<Character>();
    }

    public virtual void Tick()
    {

    }

	public virtual void Start()
	{
		
	}

    public void MoveInDirection(Vector3 Direction)
    {
        // character.rigidbody.AddForce(Direction);
        character.rigidbody.velocity = Direction;
    }

    public void Stop()
    {
        character.rigidbody.velocity = Vector3.zero;
    }
}

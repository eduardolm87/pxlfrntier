﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBrain : Brain
{
	const float ChunkOptimizerTime = 10;

	public override void Tick()
	{
		base.Tick();

		ReadPlayerInput();
	}


	void ReadPlayerInput()
	{
		ReadMouse();
	}

	

	void ReadMouse()
	{
		if (Input.GetMouseButton(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			var MousePositionInWorld = Input.mousePosition;
			MousePositionInWorld.z = 0;
			MousePositionInWorld = Camera.main.ScreenToWorldPoint(MousePositionInWorld);

			SetMovementTarget(MousePositionInWorld);

			//OLD
			//Vector3 myPos = transform.position;
			//myPos.z = 0;
			//
			//Vector3 MovementDir = (MousePositionInWorld - myPos).normalized;
			//MovementDir *= character.stats.Speed;
			//
			//MoveInDirection(MovementDir);
		}

	}


	public override void Start()
	{
		base.Start();

		InvokeRepeating("ChunkOptimizer", 1, ChunkOptimizerTime);
	}

	void ChunkOptimizer()
	{
		//todo: detects where the player is and deactivates all chunks that are not around it

		GameObject CurrentChunk = GameManager.Instance.ScenarioGenerator.GetChunkFromCoordinates(transform.position);

		if (CurrentChunk == null)
			return;

		string rawName = CurrentChunk.name.Remove(0, 6);
		string[] coords = rawName.Split(',');
		int cX = -1;
		int.TryParse(coords[0], out cX);
		int cY = -1;
		int.TryParse(coords[1], out cY);


		List<string> ChunksToKeep = new List<string>();
		for (int i = cX - 1; i <= cX + 1; i++)
		{
			for (int j = cY - 1; j <= cY + 1; j++)
			{
				ChunksToKeep.Add("Chunk " + i + "," + j);
			}
		}

		//Debug.Log("Deactivating. Exceptions: " + string.Join("/", ChunksToKeep.ToArray()));
		GameManager.Instance.ScenarioGenerator.DeactivateChunks(ChunksToKeep);
	}

}

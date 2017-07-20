using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBrain : Brain
{
	const float ChunkOptimizerTime = 10;

	public override void Tick()
	{
		base.Tick();

		//ReadPlayerInput();

		RayCastMouse();
	}


	void RayCastMouse()
	{
		if (!Input.GetMouseButtonDown(0))
			return;

		GameManager.Instance.HUD.Infopanel.Hide();
		
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(ray, out hit);

		if (hit.collider != null)
		{
			Character character = hit.collider.GetComponent<Character>();
			if (character != null)
			{
				TouchCharacter(character);
			}
			else if (hit.collider.tag == "tile")
			{
				TouchTile(hit.collider.gameObject);
			}
		}
	}

	void TouchTile(GameObject target)
	{
		//Debug.Log("touched tile " + target.name);	
		GameManager.Instance.ScenarioGenerator.HighlightOnlyOneTile(target, Color.yellow);

		GameManager.Instance.Camera.target = target.transform;

		GameManager.Instance.HUD.Infopanel.ShowInfoFromTile(target);
	}

	void TouchCharacter(Character target)
	{
		Debug.Log("touched character " + target.name);
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

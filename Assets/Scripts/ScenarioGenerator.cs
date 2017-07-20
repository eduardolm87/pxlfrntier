using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScenarioGenerator : MonoBehaviour
{
	[System.Serializable]
	public class RandomTile
	{
		public Sprite sprite = null;
		public int ids = 0;
	}

	[System.Serializable]
	public class RandomPrefab
	{
		public GameObject prefab = null;
		public float chance = 0;
	}

	//Setup
	public int ChunkSides = 2;
	public int ChunkWidth = 20;
	public int ChunkHeight = 20;
	public float TerrainZ = 1;
	public float ElementsZ = 0;
	public float SizeOfTile = 1.25f;
	public Vector3 LevelRotation = Vector3.zero;
	public Vector3 LevelPosition = Vector3.zero;

	//Prefab references
	public List<RandomTile> Tiles = new List<RandomTile>();
	public List<RandomPrefab> Elements = new List<RandomPrefab>();

	//Externals
	[HideInInspector]
	public List<GameObject> Chunks = new List<GameObject>();

	//Auxiliars
	int TerrainTotalWeight = 0;
	int ElementsTotalWeight = 0;
	int CurrentChunkX = 0;
	int CurrentChunkY = 0;
	float ChunkOffsetX = 0;
	float ChunkOffsetY = 0;

	public Transform ScenarioParent;
	Transform ChunkParent;
	Transform Chunk_TerrainParent;
	Transform Chunk_ElementsParent;

	public void Generate()
	{
		//Debug.Log("Generating...");
		StartCoroutine(GenerateTerrainCoroutine());
	}



	IEnumerator GenerateTerrainCoroutine()
	{
		yield return new WaitForEndOfFrame();

		GlobalPreCalculations();

      
		for (int chunkI = 0; chunkI < ChunkSides; chunkI++)
		{
			for (int chunkJ = 0; chunkJ < ChunkSides; chunkJ++)
			{
				CurrentChunkX = chunkI;
				CurrentChunkY = chunkJ;

				ChunkPreCalculations();

				ChunkGenerate();
			}
		}
			
		yield return new WaitForEndOfFrame();

		GlobalPostCalculations();

		yield return new WaitForEndOfFrame();

	
	}

	void GlobalPostCalculations()
	{
		ScenarioParent.transform.rotation = Quaternion.Euler(LevelRotation);
	}

	void ChunkGenerate()
	{
		//Terrain
		for (int i = 0; i < ChunkWidth; i++)
		{

			for (int j = 0; j < ChunkHeight; j++)
			{
				GenerateTerrain(i, j);
			}
		}

		//Elements
		for (int i = 0; i < ChunkWidth; i++)
		{

			for (int j = 0; j < ChunkHeight; j++)
			{
				GenerateElement(i, j);
			}
		}
	}

	void GlobalPreCalculations()
	{
		TerrainTotalWeight = 0;
		TerrainTotalWeight = Mathf.Max(Tiles.ConvertAll(x => Mathf.RoundToInt(x.ids)).ToArray());

		GameObject scenarioParent = new GameObject();
		scenarioParent.name = "Scenario";
		scenarioParent.transform.SetParent(null);
		scenarioParent.transform.position = LevelPosition;
		ScenarioParent = scenarioParent.transform;


		Chunks.Clear();
	}

	void ChunkPreCalculations()
	{
		GameObject chunkParent = new GameObject();
		chunkParent.name = "Chunk " + CurrentChunkX + "," + CurrentChunkY;
		chunkParent.transform.SetParent(ScenarioParent);
		chunkParent.transform.position = Vector3.zero;
		ChunkParent = chunkParent.transform;
		Chunks.Add(chunkParent);

		GameObject newObj = new GameObject();
		newObj.name = "Terrain";
		newObj.transform.SetParent(ChunkParent);
		newObj.transform.position = Vector3.zero;
		Chunk_TerrainParent = newObj.transform;

		GameObject newObj2 = new GameObject();
		newObj2.name = "Elements";
		newObj2.transform.SetParent(ChunkParent);
		newObj2.transform.position = Vector3.zero;
		Chunk_ElementsParent = newObj2.transform;


		//Chunk offset
		ChunkOffsetX = CurrentChunkX * ChunkWidth * SizeOfTile;
		ChunkOffsetY = CurrentChunkY * ChunkHeight * SizeOfTile;

	}





	void GenerateTerrain(int x, int y)
	{
		RandomTile piece = GetRandomTile();
		if (piece == null)
			return;

		Sprite spriteToUse = piece.sprite;


		GameObject newTile = new GameObject(spriteToUse.name);
		SpriteRenderer newTileRenderer = newTile.AddComponent<SpriteRenderer>();
		newTileRenderer.sprite = spriteToUse;

		Collider newTileCollider = newTile.AddComponent<BoxCollider>();

		newTile.transform.SetParent(Chunk_TerrainParent, false);

		newTile.tag = "tile";

		Vector3 tilePosition = new Vector3(SizeOfTile * x, SizeOfTile * y, TerrainZ);
		tilePosition.x += ChunkOffsetX;
		tilePosition.y += ChunkOffsetY;
		newTile.transform.position = tilePosition;

	}

	void GenerateElement(int x, int y)
	{
		RandomPrefab piece = GetRandomElement();
		if (piece == null)
			return;

		Vector3 tilePosition = new Vector3(SizeOfTile * x, SizeOfTile * y, ElementsZ);
		tilePosition.x += ChunkOffsetX;
		tilePosition.y += ChunkOffsetY;

		GameObject newElement = GameObject.Instantiate(piece.prefab, tilePosition, Quaternion.identity) as GameObject;
		newElement.transform.SetParent(Chunk_ElementsParent);
	}

	RandomTile GetRandomTile()
	{
		if (Tiles.Count < 1)
			return null;

		int r = Random.Range(0, TerrainTotalWeight);

		RandomTile p = null;

		for (int ind = 0; ind < Tiles.Count && p == null; ind++)
		{
			if (r <= Tiles[ind].ids)
			{
				p = Tiles[ind];
			}
		}

		if (p == null)
		{
			Debug.Log("Not found tile.");
		}

		//Debug.Log("RandomTile. Random Number=" + r + "    Piece:" + p.sprite.name);

		return p;
	}

	RandomPrefab GetRandomElement()
	{
		foreach (var element in Elements)
		{
			float r = Random.Range(0f, 1f);

			if (r <= element.chance)
			{
				return element;
			}
		}

		return null;
	}


	public void DestroyScenario()
	{
		Chunks.Clear();
		TilesAffected.Clear();

		Destroy(ScenarioParent.gameObject);
	}


	public GameObject GetChunkFromCoordinates(Vector2 Coordinates)
	{
		float x = Coordinates.x;
		float y = Coordinates.y;

		float tilesX = x / SizeOfTile;
		float chunksX = Mathf.FloorToInt(tilesX * 1f / ChunkWidth);

		float tilesY = y / SizeOfTile;
		float chunksY = Mathf.FloorToInt(tilesY * 1f / ChunkHeight);


		string ChunkName = "Chunk " + chunksX + "," + chunksY;
		return Chunks.FirstOrDefault(c => c.name == ChunkName);
	}

	public void DeactivateChunks(List<string> Exceptions)
	{
		Chunks.ForEach(c =>
		{
			c.SetActive(Exceptions.Contains(c.name));

		});
	}




	List<GameObject> TilesAffected = new List<GameObject>();


	public void HighlightTile(GameObject tile, Color color)
	{
		var outline = tile.AddComponent<cakeslice.Outline>();
		outline.color = 1;
		GameManager.Instance.Camera.OutlineEffect.lineColor0 = color;
		TilesAffected.Add(tile);
	}

	public void HighlightOnlyOneTile(GameObject tile, Color color)
	{
		ClearAllHighlightedTiles();
		HighlightTile(tile, color);
	}

	public void ClearTile(GameObject tile)
	{
		var outline = tile.GetComponent<cakeslice.Outline>();
		Destroy(outline);
	}

	public void ClearAllHighlightedTiles()
	{
		TilesAffected.ForEach(t =>
		{
			var o = t.GetComponent<cakeslice.Outline>();
			Destroy(o);
		});

		TilesAffected.Clear();
	}
}

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

    public int W = 20;
    public int H = 20;
    public float TerrainZ = 1;
    public float ElementsZ = 0;
    public float SizeOfTile = 16;

    public Transform ScenarioParent;
    public Transform TerrainParent;



    public List<RandomTile> Tiles = new List<RandomTile>();
    public List<RandomPrefab> Elements = new List<RandomPrefab>();




    int TerrainTotalWeight = 0;
    int ElementsTotalWeight = 0;

    public void Generate()
    {
        Debug.Log("Generating...");
        StartCoroutine(GenerateTerrainCoroutine());
    }



    IEnumerator GenerateTerrainCoroutine()
    {

        PreCalculations();

        //Terrain
        for (int i = 0; i < W; i++)
        {

            for (int j = 0; j < H; j++)
            {
                GenerateTerrain(i, j);
            }
        }

        //Elements
        for (int i = 0; i < W; i++)
        {

            for (int j = 0; j < H; j++)
            {
                GenerateElement(i, j);
            }
        }

        yield return new WaitForEndOfFrame();
    }

    void PreCalculations()
    {
        TerrainTotalWeight = 0;
        TerrainTotalWeight = Mathf.Max(Tiles.ConvertAll(x => Mathf.RoundToInt(x.ids)).ToArray());
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

        newTile.transform.SetParent(TerrainParent, false);

        Vector3 tilePosition = new Vector3(SizeOfTile * x, SizeOfTile * y, TerrainZ);
        newTile.transform.position = tilePosition;

    }

    void GenerateElement(int x, int y)
    {
        RandomPrefab piece = GetRandomElement();
        if (piece == null)
            return;

        Vector3 tilePosition = new Vector3(SizeOfTile * x, SizeOfTile * y, ElementsZ);

        GameObject newElement = GameObject.Instantiate(piece.prefab, tilePosition, Quaternion.identity) as GameObject;
        newElement.transform.SetParent(ScenarioParent);
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
}

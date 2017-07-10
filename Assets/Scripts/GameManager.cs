using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;

	void Awake()
	{
		Instance = this;
	}


	public static GameManagerReferences Ref
	{
		get
		{
			return Instance.References;
		}
	}

	public GameManagerReferences References;

	public ScenarioGenerator ScenarioGenerator;

	public PlayerInventory PlayerInventory;

	public HUDController HUD;

	[HideInInspector]
	public Character Player;



	void Start()
	{
		StartGame();
	}


	public void Update()
	{
		Cheats();
	}

	void Cheats()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Reset();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			PlayerInventory.AddToInventory("Candle");
		}
	}


	public void StartGame()
	{
		ScenarioGenerator.Generate();

		InstantiatePlayer();

		PlayerInventory.transform.root.gameObject.SetActive(true);
	}

	public void Reset()
	{
		ScenarioGenerator.DestroyScenario();

		PlayerInventory.Reset();

		Destroy(Player.gameObject);

		StartGame();
	}




	Character InstantiateCharacter(CharacterData data)
	{
		GameObject o = Instantiate(Ref.CharacterPrefab.gameObject) as GameObject;
		Character c = o.GetComponent<Character>();
		c.ApplyCharacterData(data);

		return c;
	}


	Character InstantiateCharacter(string charName)
	{
		CharacterData pfb = Ref.Characters.FirstOrDefault(x => x.name == charName);
		if (pfb == null)
		{
			Debug.LogError("Not found character " + charName);
			return null;
		}

		return InstantiateCharacter(pfb);
	}

	void InstantiatePlayer()
	{
		Player = InstantiateCharacter("cowgirl"); //todo: default
		Player.brain = Player.gameObject.AddComponent<PlayerBrain>();
		Player.IsPlayer = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
	//Other
	public Text FloatingText;

	//Inventory
	public GameObject InventoryItemOptionsMenu;
	public GameObject InventoryItemOptionsButton;
	List<GameObject> InventoryItemOptionsMenuButtons = new List<GameObject>();

	//Infopanel
	public Infopanel Infopanel;

	//Header
	public HudStatBar HealthBar;
	public HudStatBar HungerBar;
	public HudStatBarWeapon WeaponBar;
	public Text GoldBar;


	public void OpenInventoryOptionsOverItem(InventoryItem slot)
	{
		InventoryItemOptionsMenu.transform.position = slot.transform.position;

		PopulateInventoryOptions(slot);

		InventoryItemOptionsMenu.SetActive(true);

	}

	public void CloseInventoryOptionsOverItem()
	{
		InventoryItemOptionsMenu.SetActive(false);
	}

	void PopulateInventoryOptions(InventoryItem slot)
	{
		RemoveAllInventoryOptions();

		AddInventoryOptionsButton("Drop");

		//todo: add the rest
		AddInventoryOptionsButton("Drop2");
		AddInventoryOptionsButton("Drop3");

	}

	void AddInventoryOptionsButton(string Command)
	{
		GameObject o = Instantiate(InventoryItemOptionsButton) as GameObject;
		o.transform.SetParent(InventoryItemOptionsMenu.transform);
		o.transform.localScale = Vector3.one;

		Text t = o.GetComponentInChildren<Text>();
		t.text = Command;
	}

	void RemoveAllInventoryOptions()
	{
		while (InventoryItemOptionsMenuButtons.Count > 0)
		{
			Destroy(InventoryItemOptionsMenuButtons[0]);
			InventoryItemOptionsMenuButtons.RemoveAt(0);
		}
	}

}

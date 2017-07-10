using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
	public Transform InventoryListParent;
	public GameObject InventoryItemPrefab;

	List<InventoryItem> Slots = new List<InventoryItem>();

	public void AddToInventory(string Item)
	{

		GameItem item = GameManager.Ref.Items.FirstOrDefault(x => x.name == Item);
		if (item == null)
		{
			Debug.LogError("Not valid item: " + Item);
			return;
		}

		GameObject objSlot = Instantiate(InventoryItemPrefab) as GameObject;
		objSlot.transform.SetParent(InventoryListParent, false);
		InventoryItem objSlotItem = objSlot.GetComponent<InventoryItem>();
		objSlotItem.Associate(item);
		Slots.Add(objSlotItem);
	}


	public void RemoveFromInventory(InventoryItem Slot)
	{
		int index = Slots.FindIndex(x => x.GetInstanceID() == Slot.GetInstanceID());

		Destroy(Slot.gameObject);
		Slots.RemoveAt(index);
	}

	public void RemoveFromInventory(string Item)
	{
		InventoryItem slot = Slots.FirstOrDefault(x => x.AssociatedItem.name == Item);
		if (slot != null)
		{
			RemoveFromInventory(slot);
		}
	}

	public void Reset()
	{
		while (Slots.Count > 0)
		{
			Destroy(Slots[0].gameObject);
			Slots.RemoveAt(0);
		}
	}


	public List<GameItem> Items
	{
		get
		{
			return Slots.ConvertAll(x => x.AssociatedItem).ToList();
		}
	}



}

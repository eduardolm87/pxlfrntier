using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
	public Image IconRenderer;
	public GameItem AssociatedItem;

	public void OnClicked()
	{
		if (AssociatedItem == null)
		{
			Debug.Log("No item associated");
			return;
		}

		GameManager.Instance.HUD.OpenInventoryOptionsOverItem(this);

	}

	public void OnPointerEnter()
	{
		GameManager.Instance.HUD.FloatingText.text = AssociatedItem.name;
		GameManager.Instance.HUD.FloatingText.transform.position = transform.position;

	}

	public void OnPointerExit()
	{
		GameManager.Instance.HUD.FloatingText.text = "";
	}

	public void Associate(GameItem item)
	{
		AssociatedItem = item;
		IconRenderer.sprite = AssociatedItem.Icon;
	}
		
}

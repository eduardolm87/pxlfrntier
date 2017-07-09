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
        if (AssociatedItem != null)
            Debug.Log("clicked " + AssociatedItem.name);
        else
            Debug.Log("No item associated");
    }

    public void Associate(GameItem item)
    {
        AssociatedItem = item;
        IconRenderer.sprite = AssociatedItem.Icon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infopanel : MonoBehaviour
{
	public Text Label;




	public void ShowInfoFromTile(GameObject tile)
	{
		var tileInfo = GameManager.Instance.ScenarioGenerator.GetTileInfoFromGameObject(tile);

		Show(tileInfo.Name);
	}

	public void Show(string Text)
	{
		Label.text = Text;
		Show();
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}

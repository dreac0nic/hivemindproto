using UnityEngine;
using System.Collections;
using System.Text;

public class HUD : MonoBehaviour
{
	public GUISkin resourceSkin;
	public GUISkin selectionSkin;

	private Player player;

	private const int RESOURCE_BAR_WIDTH = 320;
	private const int RESOURCE_BAR_HEIGHT = 120;
	private const int SELECTION_LIST_WIDTH = 320;
	private const int SELECTION_LIST_HEIGHT = 1024;

	void Start()
	{
		player = transform.root.GetComponent<Player>();
	}

	void OnGUI()
	{
		GUI.skin = resourceSkin;
		GUI.BeginGroup(new Rect(Screen.width/2 - RESOURCE_BAR_WIDTH/2, 0, RESOURCE_BAR_WIDTH, RESOURCE_BAR_HEIGHT));
		GUI.Label(new Rect(0, 0, RESOURCE_BAR_WIDTH, RESOURCE_BAR_HEIGHT), "RESOURCES");
		GUI.EndGroup();

		GUI.skin = selectionSkin;
		GUI.BeginGroup(new Rect(0, 0, SELECTION_LIST_WIDTH, SELECTION_LIST_HEIGHT));

		GUI.Label(new Rect(0, 0, SELECTION_LIST_WIDTH, 30), "SELECTED UNITS");

		Selector compie = player.GetComponent<Selector>();
		if(compie && compie.SelectedUnits.Count > 0) {
			StringBuilder buffer = new StringBuilder();

			foreach(var obj in compie.SelectedUnits) {
				buffer.Append(obj.transform.root.gameObject.name);

				Loyal temp = obj.transform.root.GetComponent<Loyal>();
				if(temp) {
					buffer.Append(" [" + temp.loyalty + "] ");

					if(!temp.Feral)
						buffer.Append(" [" + temp.Allegiance.transform.root.gameObject.name + "]");
				}

				buffer.Append("\n");
			}

			GUI.Label(new Rect(0, 30, SELECTION_LIST_WIDTH, SELECTION_LIST_HEIGHT - 30), buffer.ToString());
		} else
			GUI.Label(new Rect(0, 30, SELECTION_LIST_WIDTH, SELECTION_LIST_HEIGHT - 30), "NO CURRENT SELECTIONS");

		GUI.EndGroup();
	}
}

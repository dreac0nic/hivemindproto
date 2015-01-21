using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	private const int SELECTION_NAME_HEIGHT = 15;

	public GUISkin resourceSkin, ordersSkin;

	private const int ORDERS_BAR_WIDTH = 150;
	private const int RESOURCE_BAR_HEIGHT = 40;

	private Player player;

	// Use this for initialization-- NO!
	void Start ()
	{
		player = transform.root.GetComponent<Player>();
	}

	void OnGUI()
	{
		if(player && player.human) {
			DrawOrdersBar();
			DrawResourceBar();
		}
	}

	private void DrawOrdersBar()
	{
		GUI.skin = ordersSkin;
		GUI.BeginGroup(new Rect(Screen.width - ORDERS_BAR_WIDTH, RESOURCE_BAR_HEIGHT, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT));
		GUI.Box(new Rect(0, 0, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT), "");

		string selectionName = "";

		if(player.SelectedObject)
			selectionName = player.SelectedObject.objectName;

		if(!selectionName.Equals(""))
			GUI.Label(new Rect(0, 10, ORDERS_BAR_WIDTH, SELECTION_NAME_HEIGHT), selectionName);

		GUI.EndGroup();
	}

	private void DrawResourceBar()
	{
		GUI.skin = resourceSkin;
		GUI.BeginGroup(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT));
		GUI.Box(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT), "");
		GUI.EndGroup();
	}

	public bool MouseInBounds()
	{
		Vector3 mousePos = Input.mousePosition;

		bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH;
		bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT;

		return insideWidth && insideHeight;
	}
}

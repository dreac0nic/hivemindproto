using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public GUISkin resourceSkin;

	private Player player;

	private const int RESOURCE_BAR_WIDTH = 320;
	private const int RESOURCE_BAR_HEIGHT = 120;

	void Start ()
	{
		player = transform.root.GetComponent<Player>();
	}

	void OnGUI()
	{
		GUI.skin = resourceSkin;
		GUI.BeginGroup(new Rect(Screen.width/2 - RESOURCE_BAR_WIDTH/2, 0, RESOURCE_BAR_WIDTH, RESOURCE_BAR_HEIGHT));
		GUI.Label(new Rect(0, 0, RESOURCE_BAR_WIDTH, RESOURCE_BAR_HEIGHT), "RESOURCES");
		GUI.EndGroup();
	}
}

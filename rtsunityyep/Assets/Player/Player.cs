using UnityEngine;
using System.Collections;

using RTS;

public class Player : MonoBehaviour
{
	public string username;
	public bool human;
	public HUD hud;

	public WorldObject SelectedObject { get; set; }

	void Start ()
	{
		hud = GetComponentInChildren<HUD>();
	}

	void Update ()
	{

	}
}

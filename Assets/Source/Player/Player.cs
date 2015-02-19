using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using HiveMind;

public class Player : MonoBehaviour
{
	// Object Variables
	public string username;
	public Influential queen;
	public Material color;

	void Start ()
	{
		queen.Player = this;
	}

	void Update ()
	{

	}
}

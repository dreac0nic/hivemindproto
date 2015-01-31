using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
	public bool Selected { get { return currentlySelected; } set { currentlySelected = value; } }

	protected bool currentlySelected;

	void Start()
	{

	}

	void Update()
	{

	}
}

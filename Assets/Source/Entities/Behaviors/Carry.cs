using UnityEngine;
using System.Collections;

public class Carry : MonoBehaviour 
{
	public bool IsFull { get { return CurrentlyCarrying >= CarryingCapacity; } }
	public int CarryingCapacity;

	protected int CurrentlyCarrying { get; set; }
	
	public bool GiveResources(int resourceAmount)
	{
		if (resourceAmount + CurrentlyCarrying > CarryingCapacity)
			return false;

		CurrentlyCarrying += resourceAmount;
		return true;
	}
}

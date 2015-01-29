using UnityEngine;
using System.Collections;

public class Harvester : MonoBehaviour
{
	// Stats
	public float HarvestingRate;
	public float HarvestRange;
	public int CarryingCapacity;
	public bool IsHarvesting { get; set; }
	public int CurrentlyCarrying { get; set; }

	private GameObject resourceBeingHarvested { get; set; }

	// Default values
	private float defaultHarvestingRate = 1.0f;
	private int defaultCarryingCapacity = 10;

	protected void Start()
	{
		HarvestingRate = defaultHarvestingRate;
		IsHarvesting = false;
		CarryingCapacity = defaultCarryingCapacity;
		CurrentlyCarrying = 0;
	}

	public void Deposit(GameObject carrier)
	{
		if (carrier.GetComponent<Carry>().GiveResources(CurrentlyCarrying))
			CurrentlyCarrying = 0;
	}

	public void StartHarvesting(GameObject resourceToHarvest)
	{
		IsHarvesting = true;
		resourceBeingHarvested = resourceToHarvest;
		InvokeRepeating("Harvest", 0, HarvestingRate);
	}

	public void StopHarvesting()
	{
		IsHarvesting = false;
		CancelInvoke("Harvest");
	}

	private void Harvest()
	{
		if (CurrentlyCarrying >= CarryingCapacity || resourceBeingHarvested.GetComponent<Harvestable>().ResourcesLeft <= 0)
			StopHarvesting();

		float distanceToResouce = Vector3.Distance(resourceBeingHarvested.transform.position, GetComponent<UnitStats>().Position);
		if (distanceToResouce <= HarvestRange)
		{
			CurrentlyCarrying += 1;
			resourceBeingHarvested.GetComponent<Harvestable>().ResourcesLeft -= 1;
		}
		else
		{
			GetComponent<Movable>().Move(resourceBeingHarvested.transform.position, HarvestRange);
		}
	}

}

using UnityEngine;
using System.Collections;

public class Harvester : MonoBehaviour
{
	// Stats
	public float HarvestingRate;
	public float HarvestRange;
	public float DepositRange;
	public float DepositRetryRate;
	public int CarryingCapacity;
	public int CurrentlyCarrying;

	private GameObject resourceBeingHarvested;
	private GameObject carrierBeingDepositedTo;

	protected void Start()
	{
		CurrentlyCarrying = 0;
	}

	public void StartDepositing(GameObject carrier)
	{
		carrierBeingDepositedTo = carrier;
		InvokeRepeating("Deposit", 0, DepositRetryRate);
	}

	public void StopDepositing()
	{
		CancelInvoke("Deposit");
	}

	public void Deposit()
	{
		float distanceToCarrier = Vector3.Distance(carrierBeingDepositedTo.transform.position, GetComponent<UnitStats>().Position);
		if (distanceToCarrier <= DepositRange)
		{
			if (carrierBeingDepositedTo.GetComponent<Carry>().GiveResources(CurrentlyCarrying))
				CurrentlyCarrying = 0;
			StopDepositing();
		}
		else
		{
			GetComponent<Movable>().Move(carrierBeingDepositedTo.transform.position, DepositRange);
		}
	}

	public void StartHarvesting(GameObject resourceToHarvest)
	{
		resourceBeingHarvested = resourceToHarvest;
		InvokeRepeating("Harvest", 0, HarvestingRate);
	}

	public void StopHarvesting()
	{
		CancelInvoke("Harvest");
	}

	private void Harvest()
	{
		if (CurrentlyCarrying >= CarryingCapacity - 1 || resourceBeingHarvested.GetComponent<Harvestable>().ResourcesLeft <= 0)
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

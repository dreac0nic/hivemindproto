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
	private Harvestable harvestFrom;
	private Carry depositTo;

	private UnitStats unitStats;
	private Movable movable;

	protected void Start()
	{
		unitStats = GetComponent<UnitStats>();
		movable = GetComponent<Movable>();
		CurrentlyCarrying = 0;
	}

	public void StartDepositing(GameObject carrier)
	{
		carrierBeingDepositedTo = carrier;
		depositTo = carrier.GetComponent<Carry>();
		InvokeRepeating("Deposit", 0, DepositRetryRate);
	}

	public void StopDepositing()
	{
		CancelInvoke("Deposit");
	}

	public void Deposit()
	{
		float distanceToCarrier = Vector3.Distance(carrierBeingDepositedTo.transform.position, unitStats.Position);
		if (distanceToCarrier <= DepositRange)
		{
			if (depositTo.GiveResources(CurrentlyCarrying))
				CurrentlyCarrying = 0;
			StopDepositing();
		}
		else
		{
			movable.Move(carrierBeingDepositedTo.transform.position, DepositRange);
		}
	}

	public void StartHarvesting(GameObject resourceToHarvest)
	{
		resourceBeingHarvested = resourceToHarvest;
		harvestFrom = resourceToHarvest.GetComponent<Harvestable>();
		InvokeRepeating("Harvest", 0, HarvestingRate);
	}

	public void StopHarvesting()
	{
		CancelInvoke("Harvest");
	}

	private void Harvest()
	{
		if (CurrentlyCarrying >= CarryingCapacity - 1 || harvestFrom.ResourcesLeft <= 0)
			StopHarvesting();

		float distanceToResouce = Vector3.Distance(resourceBeingHarvested.transform.position, unitStats.Position);
		if (distanceToResouce <= HarvestRange)
		{
			CurrentlyCarrying += 1;
			harvestFrom.ResourcesLeft -= 1;
		}
		else
		{
			movable.Move(resourceBeingHarvested.transform.position, HarvestRange);
		}
	}

}

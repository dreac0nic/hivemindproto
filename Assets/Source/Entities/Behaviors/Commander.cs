using UnityEngine;
using System.Collections;
using System.Linq;

using HiveMind;
using System.Collections.Generic;

public class Commander : MonoBehaviour
{
	// Get rid of these after testing movement
	private GameObject tempCircle;
	private Player player;

	void Start()
	{
		player = GetComponent<Player>();
	}

	void Update()
	{
		Selector selectGroup = transform.root.GetComponent<Selector>();
		
		if(selectGroup)
		{
			HashSet<Selectable> selectedUnits =  new HashSet<Selectable>(selectGroup
				.SelectedUnits
				.Where(su => su.GetComponent<Influential>() == player.queen || (su.GetComponent<Loyal>() && su.GetComponent<Loyal>().CheckLoyalty(player.queen))));

			// Breeding
			if (Input.GetKeyDown(KeyCode.B))
			{
				foreach(Breed breedComponent in selectedUnits.Select(su => su.GetComponent<Breed>()).Where(b => b != null))
				{
					if (breedComponent.IsBirthing)
						breedComponent.StopBirthing();
					else
						breedComponent.StartBirthing();
				}
			}

			// Ordering (right click)
			if (Input.GetButtonDown("Order"))
			{
				GameObject pickedObject = Selector.PickObject();
				RaycastHit hitInfo;

				if(pickedObject.GetComponent<Carry>() != null)
				{
					foreach (Harvester harvestComponent in selectedUnits.Select(su => su.GetComponent<Harvester>()).Where(h => h != null))
					{
						harvestComponent.StartDepositing(pickedObject);
					}
				}
				else if(pickedObject.GetComponent<UnitStats>() != null)
				{
					foreach (Attacker attackComponent in selectedUnits.Select(su => su.GetComponent<Attacker>()).Where(a => a != null))
					{
						attackComponent.StartAttacking(pickedObject);
					}
				}
				else if(pickedObject.GetComponent<Harvestable>() != null)
				{
					foreach (Harvester harvestComponent in selectedUnits.Select(su => su.GetComponent<Harvester>()).Where(h => h != null))
					{
						harvestComponent.StartHarvesting(pickedObject);
					}
				}
				else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, Layers.Map))
				{
					// Spawn a marker
					if (!tempCircle)
						tempCircle = GameObject.CreatePrimitive(PrimitiveType.Sphere);

					tempCircle.transform.position = hitInfo.point;
					tempCircle.transform.localScale = new Vector3(10, 10, 10);

					// Apply movement to each selected unit.
					if (selectGroup)
					{
						foreach (Selectable unit in selectGroup.SelectedUnits)
						{
							Loyal loyalty = unit.GetComponent<Loyal>();
							Influential queen = unit.GetComponent<Influential>();
							Movable action = unit.GetComponent<Movable>();

						if(action && ((loyalty && loyalty.allegiance == player.queen) || (queen && queen == player.queen)))
							action.Move(hitInfo.point);
						}
					}
				}
			}
		}
	}
}

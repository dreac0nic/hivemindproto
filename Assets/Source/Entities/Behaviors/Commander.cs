using UnityEngine;
using System.Collections;
using System.Linq;

using HiveMind;
using System.Collections.Generic;

public class Commander : MonoBehaviour
{
	private GameObject cursor;
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
			HashSet<Selectable> selectedUnits = selectGroup.SelectedUnits;

			// Breeding
			foreach(Breed breedComponent in selectedUnits.Select(su => su.GetComponent<Breed>()).Where(b => b != null))
			{
				if (Input.GetKeyDown(KeyCode.B))
				{
					if (breedComponent.IsBirthing)
						breedComponent.StopBirthing();
					else
						breedComponent.StartBirthing();
				}
			}

			// Test hurting
			if(Input.GetKeyDown(KeyCode.H))
			{
				foreach(UnitStats unit in selectedUnits.Select(su => su.GetComponent<UnitStats>()).Where(b => b != null))
				{
					if(unit.Health > 0)
						unit.Health -= 5;
					else
						unit.Health = unit.MaxHealth;
				}
			}

			// Ordering (right click)
			if (Input.GetButtonDown("Order"))
			{
				GameObject pickedObject = Selector.PickObject();
				RaycastHit hitInfo;

				// If the picked object is a unit, attack it!
				if(pickedObject.GetComponent<UnitStats>() != null)
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
					if (!cursor)
						cursor = (GameObject)Instantiate(Resources.Load("Cursor"));
					else
					{
						Destroy (cursor);
						cursor = (GameObject) Instantiate(Resources.Load("Cursor"));
					}

					cursor.transform.position = hitInfo.point;
					cursor.transform.localScale = new Vector3(1, 1, 1);
					Destroy(cursor, .6f);

					// Apply movement to each selected unit.
					if (selectGroup)
					{
						foreach (Selectable unit in selectGroup.SelectedUnits)
						{
							Movable action = unit.GetComponent<Movable>();

							if (action)
								action.Move(hitInfo.point);
						}
					}
				}
			}
		}
	}
}

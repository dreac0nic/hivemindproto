using UnityEngine;
using System.Collections;
using System.Linq;

using HiveMind;
using System.Collections.Generic;

public class Commander : MonoBehaviour
{
	public Vector3 INFINITY { get { return new Vector3(-99999, -99999, -99999); } }

	public HashSet<Commandable> SelectedUnits { get { return selected; }}

	protected HashSet<Commandable> selected = new HashSet<Commandable>();
	protected Player player;
	private GameObject cursor;

	void Start()
	{
		player = GetComponent<Player>();
	}

	void Update()
	{
		if(Input.GetButtonDown("Select")) {
			GameObject picked = PickObject();
			Vector3 hitPoint = FindRayCollision();
			Commandable commandee = picked.GetComponentInParent<Commandable>();

			if(hitPoint != INFINITY && picked && commandee) {
				if(!Input.GetButton("SelectModifier"))
					ClearSelection();

				if(!selected.Contains(commandee)) {
					commandee.Selected = true;
					selected.Add(commandee);
				}
			} else
				ClearSelection();
		}
		// OLD CODE
		/*
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
		*/
	}

	// Helper Methods for Finding Picks
	public static GameObject PickObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.collider.gameObject.transform.parent.gameObject;

		return null;
	}

	Vector3 FindRayCollision()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.point;

		return INFINITY;
	}

	void ClearSelection()
	{
		foreach(Commandable unit in selected)
			unit.Selected = false;

		selected.Clear();
	}
}

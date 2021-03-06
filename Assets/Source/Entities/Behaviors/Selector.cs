using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour
{
	public HashSet<Selectable> SelectedUnits { get { return selected; }}

	protected HashSet<Selectable> selected = new HashSet<Selectable>();

	void Start()
	{

	}

	void Update()
	{
		if(!enabled) return;

		if(Input.GetButtonDown("Select")) {
			GameObject pickedObject = PickObject();
			Vector3 hitPoint = FindRayCollision();
			Selectable objectSelect = pickedObject.GetComponentInParent<Selectable>();


			if(pickedObject && hitPoint != new Vector3(-99999, -99999, -99999) && objectSelect && !selected.Contains(objectSelect)) {
				if(!Input.GetButton("SelectModifier"))
					ClearSelection();

				objectSelect.Selected = true;
				selected.Add(objectSelect);
			} else {
				ClearSelection();
			}
		}
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

		return new Vector3(-99999, -99999, -99999);
	}

	void ClearSelection()
	{
		foreach(Selectable unit in selected)
			unit.Selected = false;

		selected.Clear();
	}
}

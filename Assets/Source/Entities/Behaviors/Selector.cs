using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour
{
	public LinkedList<Selectable> SelectedUnits { get { return selected; }}

	protected LinkedList<Selectable> selected = new LinkedList<Selectable>();

	void Start()
	{

	}

	void Update()
	{
		if(!enabled) return;

		if(Input.GetMouseButtonDown(0)) {
			GameObject pickedObject = PickObject();
			Vector3 hitPoint = FindRayCollision();

			if(pickedObject && hitPoint != new Vector3(-99999, -99999, -99999)) {
				Selectable objectSelect = pickedObject.transform.root.GetComponent<Selectable>();

				if(objectSelect) {
					objectSelect.Selected = true;
					selected.AddLast(objectSelect);
				}
			}
		} else if (Input.GetMouseButtonDown(1)) {
			foreach(var obj in selected)
				obj.Selected = false;

			selected.Clear();
		}
	}

	// Helper Methods for Finding Picks
	GameObject PickObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.collider.gameObject;

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
}

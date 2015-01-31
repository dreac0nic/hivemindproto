using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	public bool SelectedUnits { get { return selected; }}

	protected LinkedList<GameObject> selected = new LinkedList<GameObject>();
	protected bool enabled;

	void Start()
	{

	}

	void Update()
	{
		if(!enabled) return;

		if(Input.GetMouseButtonDown(0)) {
			GameObject pickedObject = FindHitObjects();
			Vector3 hitPoint = FindHitPoint();

			if(pickedObject && hitPoint != Vector3(-99999, -99999, -99999)) {
				Selectable objectSelect = pickedObject.getComponent<Selectable>();

				if(objectSelect) {
					objectSelect = true;
					selected.AddLast(GameObject)
				}
			}
		} else if (Input.GetMoustButtonDown(1)) {
			foreach(var obj in selected)
				selected.Selected = false;

			selected.Clear();
		}
	}

	// Helper Methods for Finding Picks
	protected GameObject PickObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.collider.gameObject;

		return null;
	}

	protected Vector3 FindRayCollision()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.point;

		return Vector3(-99999, -99999, -99999);
	}
}

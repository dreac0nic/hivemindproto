using UnityEngine;
using System.Collections;

using HiveMind;

public class Commander : MonoBehaviour
{
	// Get rid of these after testing movement
	private GameObject tempCircle;

	void Start()
	{
	}

	void Update()
	{
		// Test for clicks
		if (Input.GetButtonDown("Move"))
		{
			RaycastHit hitInfo;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, Layers.Map))
			{
				// Spawn a marker
				if(!tempCircle)
				{
					tempCircle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				}
				tempCircle.transform.position = hitInfo.point;
				tempCircle.transform.localScale = new Vector3(10, 10, 10);

				// Move the unit to the point
				//moveUnit.destination = hitInfo.point;

				// Apply movement to each selected unit.
				Selector selectGroup = transform.root.GetComponent<Selector>();

				if(selectGroup) {
					foreach(Selectable unit in selectGroup.SelectedUnits) {
						Movable action = unit.transform.root.GetComponent<Movable>();

						if(action)
							action.destination = hitInfo.point;
					}
				}
			}
		}
	}
}

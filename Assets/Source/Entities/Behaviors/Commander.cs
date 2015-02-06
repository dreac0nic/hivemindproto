using UnityEngine;
using System.Collections;

using HiveMind;

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
		// Test for clicks
		if (Input.GetButtonDown("Move"))
		{
			RaycastHit hitInfo;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, Layers.Map))
			{
				// Spawn a marker
				if(!tempCircle)
					tempCircle = GameObject.CreatePrimitive(PrimitiveType.Sphere);

				tempCircle.transform.position = hitInfo.point;
				tempCircle.transform.localScale = new Vector3(10, 10, 10);

				// Move the unit to the point
				Selector selectGroup = transform.root.GetComponent<Selector>();

				if(selectGroup) {
					foreach(Selectable unit in selectGroup.SelectedUnits) {
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

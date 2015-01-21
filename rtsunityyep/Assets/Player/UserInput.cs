using UnityEngine;
using System.Collections;

using RTS;

public class UserInput : MonoBehaviour
{
	private Player player;

	// Use this for initialization
	void Start ()
	{
		player = transform.root.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {
		if(player.human) {
			MoveCamera();
			RotateCamera();
			MouseActivity();
		}
	}

	private void MouseActivity()
	{
		if(Input.GetMouseButtonDown(0)) LeftMouseClick();
		else if(Input.GetMouseButtonDown(1)) RightMouseClick();
	}

	private void LeftMouseClick()
	{
		if(player.hud.MouseInBounds()) {
			GameObject hitObject = FindHitObject();
			Vector3 hitPoint = FindHitPoint();

			if(hitObject && hitPoint != ResourceManager.InvalidPosition) {
				if(player.SelectedObject)
					player.SelectedObject.MouseClick(hitObject, hitPoint, player);
				else if(hitObject.name != "Ground") {
					WorldObject worldObject = hitObject.transform.root.GetComponent<WorldObject>();

					if(worldObject) {
						player.SelectedObject = worldObject;
						worldObject.SetSelection(true);
					}
				}
			}
		}
	}

	private void RightMouseClick()
	{
		if(player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject) {
			player.SelectedObject.SetSelection(false);
			player.SelectedObject = null;
		}
	}

	private void MoveCamera()
	{
		float xPos = Input.mousePosition.x;
		float yPos = Input.mousePosition.y;

		Vector3 movement = new Vector3(0, 0, 0);

		// Horizontal movement test...
		if(xPos >= 0 && xPos < ResourceManager.ScrollWidth)
			movement.x -= ResourceManager.ScrollSpeed;
		else if(xPos <= Screen.width && xPos > Screen.width - ResourceManager.ScrollWidth)
			movement.x += ResourceManager.ScrollSpeed;

		// Vertical camera movement
		if(yPos >= 0 && yPos < ResourceManager.ScrollWidth)
			movement.z -= ResourceManager.ScrollSpeed;
		else if(yPos <= Screen.height && yPos > Screen.height - ResourceManager.ScrollWidth)
			movement.z += ResourceManager.ScrollSpeed;

		// Realign movement to transform camera locally.
		movement = Camera.main.transform.TransformDirection(movement);
		movement.y = 0;

		// Scroll wheel control for "zooming"
		movement.y = -ResourceManager.ScrollSpeed*Input.GetAxis("Mouse ScrollWheel");

		// Calculate final destination of camera.
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin;

		destination += movement;

		// Correct ground movement.
		if(destination.y > ResourceManager.MaxCameraHeight)
			destination.y = ResourceManager.MaxCameraHeight;
		else if(destination.y < ResourceManager.MinCameraHeight)
			destination.y = ResourceManager.MinCameraHeight;

		// Apply movement if within proper qualities.
		if(destination != origin)
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime*ResourceManager.ScrollSpeed);
	}

	private void RotateCamera()
	{
		// Maybe implement later
	}

	private GameObject FindHitObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.collider.gameObject;

		return null;
	}

	private Vector3 FindHitPoint()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			return hit.point;

		return ResourceManager.InvalidPosition;
	}
}

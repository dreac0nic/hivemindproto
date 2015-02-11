using UnityEngine;
using System.Collections;

using HiveMind;

public class InputController : MonoBehaviour
{
	private Player player;

	void Start ()
	{
		// Get the player object.
		player = transform.root.GetComponent<Player>();

		// Set original camera position and rotation
		Camera.main.transform.position = Options.O.CameraStartPos;
		Camera.main.transform.rotation = Quaternion.Euler(Options.O.CameraStartRot);
	}

	void Update ()
	{
		// Update Camera Controls
		Vector2 mousePos = Input.mousePosition;
		Vector3 camTransform = new Vector3(0, 0, 0);

		// Test Horizontal Movement
		if(mousePos.x >= 0 && mousePos.x < Options.O.ScrollBorder)
			camTransform.x -= Options.O.ScrollSpeed;
		else if(mousePos.x <= Screen.width && mousePos.x > Screen.width - Options.O.ScrollBorder)
			camTransform.x += Options.O.ScrollSpeed;

		// Test Vertical Movement
		if(mousePos.y >= 0 && mousePos.y < Options.O.ScrollBorder)
			camTransform.z -= Options.O.ScrollSpeed;
		else if(mousePos.y <= Screen.height && mousePos.y > Screen.height - Options.O.ScrollBorder)
			camTransform.z += Options.O.ScrollSpeed;

		// Test arrow controls
		camTransform.z += Input.GetAxis("Vertical") * Options.O.ScrollSpeed;
		camTransform.x += Input.GetAxis("Horizontal") * Options.O.ScrollSpeed;

		// Retransform movement to camera's local orientation, drop vertical movement.
		camTransform = Camera.main.transform.TransformDirection(camTransform);
		camTransform.y = 0;

		// Zoom with scroll wheel
		if (Input.GetAxis("Zoom") != 0)
		{
			float camFieldOfView = Camera.main.fieldOfView - (Options.O.ZoomSpeed * Input.GetAxis("Zoom"));
			Camera.main.fieldOfView = Mathf.Clamp(camFieldOfView, Options.O.ZoomMin, Options.O.ZoomMax);
		}

		// Apply the movement to current position and set camera.
		Vector3 originalPos = Camera.main.transform.position;
		Vector3 destination = originalPos + camTransform;

		if(destination != originalPos)
			Camera.main.transform.position = Vector3.MoveTowards(originalPos, destination, Time.deltaTime*Options.O.ScrollSpeed);
	}
}

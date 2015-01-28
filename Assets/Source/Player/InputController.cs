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
		Camera.main.transform.position = Options.CameraStartPos;
		Camera.main.transform.rotation = Quaternion.Euler(Options.CameraStartRot);
	}

	void Update ()
	{
		// Update Camera Controls
		Vector2 mousePos = Input.mousePosition;
		Vector3 camTransform = new Vector3(0, 0, 0);

		// Test Horizontal Movement
		if(mousePos.x >= 0 && mousePos.x < Options.ScrollBorder)
			camTransform.x -= Options.ScrollSpeed;
		else if(mousePos.x <= Screen.width && mousePos.x > Screen.width - Options.ScrollBorder)
			camTransform.x += Options.ScrollSpeed;

		// Test Vertical Movement
		if(mousePos.y >= 0 && mousePos.y < Options.ScrollBorder)
			camTransform.z -= Options.ScrollSpeed;
		else if(mousePos.y <= Screen.height && mousePos.y > Screen.height - Options.ScrollBorder)
			camTransform.z += Options.ScrollSpeed;

		// Test arrow controls
		camTransform.z += Input.GetAxis("Vertical") * Options.ScrollSpeed;
		camTransform.x += Input.GetAxis("Horizontal") * Options.ScrollSpeed;

		// Retransform movement to camera's local orientation, drop vertical movement.
		camTransform = Camera.main.transform.TransformDirection(camTransform);
		camTransform.y = 0;

		// Zoom with scroll wheel
		if (Input.GetAxis("Zoom") != 0)
		{
			float camFieldOfView = Camera.main.fieldOfView - (Options.ZoomSpeed * Input.GetAxis("Zoom"));
			Camera.main.fieldOfView = Mathf.Clamp(camFieldOfView, Options.ZoomMin, Options.ZoomMax);
		}

		// Apply the movement to current position and set camera.
		Vector3 originalPos = Camera.main.transform.position;
		Vector3 destination = originalPos + camTransform;

		if(destination != originalPos)
			Camera.main.transform.position = Vector3.MoveTowards(originalPos, destination, Time.deltaTime*Options.ScrollSpeed);
	}
}

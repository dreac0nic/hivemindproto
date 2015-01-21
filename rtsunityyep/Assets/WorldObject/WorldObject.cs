using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour
{
	public string objectName;
	public Texture2D buildImage;
	public int cost, sellValue, hitPoints, maxHitPoints;

	protected Player player;
	protected string[] actions = {};
	protected bool currentlySelected = false;

	protected virtual void Awake() {}
	protected virtual void Start()
		{ player = transform.root.GetComponentInChildren<Player>(); }
	protected virtual void Update() {}
	protected virtual void OnGUI() {}

	public void SetSelection(bool selected)
		{ currentlySelected = selected; }

	public string[] GetActions() { return actions; }
	public virtual void PerformAction(string actionToPerform) {}

	public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
	{
		if(currentlySelected && hitObject && hitObject.name != "Ground") {
			WorldObject worldObject = hitObject.transform.root.GetComponent<WorldObject>();

			if(worldObject)
				ChangeSelection(worldObject, controller);
		}
	}

	private void ChangeSelection(WorldObject worldObject, Player controller)
	{
		SetSelection(false);

		if(controller.SelectedObject)
			controller.SelectedObject.SetSelection(false);

		controller.SelectedObject = worldObject;
		worldObject.SetSelection(true);
	}
}

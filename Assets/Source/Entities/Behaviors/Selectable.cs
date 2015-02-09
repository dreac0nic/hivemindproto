using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
	public GameObject SelectMarker;
	public bool Selected {
		get {
			return currentlySelected;
		}
		set {
			if(value && !currentlySelected) { addSelectMarker(); }
			else if(currentlySelected) { removeSelectMarker(); }
			currentlySelected = value;
		}
	}

	protected bool currentlySelected;
	private GameObject selectMarker;

	void Start()
	{
	}

	void Update()
	{

	}

	void addSelectMarker()
	{
		selectMarker = (GameObject)Instantiate(SelectMarker, Vector3.zero, Quaternion.identity);
		selectMarker.transform.SetParent(transform, false);

		// Hard-coded rotation of the select marker
		// TODO: Figure out how to encode this in the select marker prefab
		selectMarker.transform.Rotate(new Vector3(90, 0, 0));
	}

	void removeSelectMarker()
	{
		Destroy(selectMarker);
	}
}

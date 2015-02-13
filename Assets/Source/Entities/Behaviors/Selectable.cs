using UnityEngine;
using System.Collections;

using HiveMind;

public class Selectable : MonoBehaviour
{
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
	private GameObject unitUi;
	private RectTransform hpBarRatio;
	private UnitStats unitStats;

	void Start()
	{
		unitStats = GetComponent<UnitStats>();
	}

	void Update()
	{
		if(unitUi && hpBarRatio)
		{
			hpBarRatio.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 512f * (float)unitStats.Health / (float)unitStats.MaxHealth);
			unitUi.transform.LookAt(new Vector3(transform.position.x, Options.O.UICamera.transform.position.y, transform.position.z));
		}
	}

	void addSelectMarker()
	{
		selectMarker = (GameObject)Instantiate(Options.O.SelectMarker, Vector3.zero, Quaternion.identity);
		selectMarker.transform.SetParent(transform, false);

		unitUi = (GameObject)Instantiate(Resources.Load("UnitUI"), Vector3.up * 7, Quaternion.identity);
		unitUi.transform.SetParent(transform, false);
		hpBarRatio = unitUi.transform.Find("HpRatio").gameObject.GetComponent<RectTransform>();

		// Hard-coded rotation of the select marker
		// TODO: Figure out how to encode this in the select marker prefab
		selectMarker.transform.Rotate(new Vector3(90, 0, 0));
	}

	void removeSelectMarker()
	{
		Destroy(selectMarker);
		Destroy(unitUi);
		hpBarRatio = null;
	}
}

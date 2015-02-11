using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Influential : MonoBehaviour
{
	private static HashSet<Influential> queens = new HashSet<Influential>();
	public static HashSet<Influential> Queens {
		get {
			return queens;
		}
	}

	/* A single "strength" modifier. This encompasses both the power and distance of transmission.
	* For now, merely specifies distance, but later the algorithm should be tweaked appropriately.
	*/
	public float strength = 100;

	private GameObject influenceBubble = null;
	private Player player;

	public Player Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}

	void Start()
	{
		influenceBubble = (GameObject)Instantiate(Resources.Load("InfluenceBubble"));

		influenceBubble.transform.parent = transform;

		influenceBubble.transform.localPosition = new Vector3(0f, 0f, 0f);

		Influential.queens.Add(this);
	}

	void Update()
	{
		if(influenceBubble)
			influenceBubble.transform.localScale = 0.5f*(new Vector3(strength, strength, strength));
	}

	void OnDisable()
	{
		if(Influential.queens.Contains(this))
			Influential.queens.Remove(this);
	}

	void OnDestroy()
	{
		if(Influential.queens.Contains(this))
			Influential.queens.Remove(this);
	}

	// Returns the power at the given point of the influence bubble.
	public float Power(Vector3 pos)
	{
		float distance = Vector3.Distance(this.transform.position, pos);

		return -distance + strength;
	}
}

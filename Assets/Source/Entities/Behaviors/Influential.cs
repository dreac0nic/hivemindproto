using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Influential : MonoBehaviour
{

	/* A single "strength" modifier. This encompasses both the power and distance of transmission.
	* For now, merely specifies distance, but later the algorithm should be tweaked appropriately.
	*/
	public float strength = 100;

	private Renderer renderer;
	private GameObject influenceBubble = null;

	public Player Player { get; set; }

	public static HashSet<Influential> Queens {	get; private set; }

	void Start()
	{
		influenceBubble = (GameObject)Instantiate(Resources.Load("InfluenceBubble"));

		influenceBubble.transform.parent = transform;

		influenceBubble.transform.localPosition = new Vector3(0f, 0f, 0f);

		renderer = GetComponentInChildren<Renderer>();

		if(Queens == null)
			Queens = new HashSet<Influential>();

		Influential.Queens.Add(this);
	}

	void Update()
	{
		renderer.material = Player.color;

		if(influenceBubble)
			influenceBubble.transform.localScale = 0.5f*(new Vector3(strength, strength, strength));
	}

	void OnDisable()
	{
		if(Influential.Queens.Contains(this))
			Influential.Queens.Remove(this);
	}

	void OnDestroy()
	{
		if(Influential.Queens.Contains(this))
			Influential.Queens.Remove(this);
	}

	// Returns the power at the given point of the influence bubble.
	public float Power(Vector3 pos)
	{
		float distance = Vector3.Distance(this.transform.position, pos);

		return -distance + strength;
	}
}

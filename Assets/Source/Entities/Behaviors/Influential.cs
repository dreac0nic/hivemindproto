using UnityEngine;
using System.Collections;

public class Influential : MonoBehaviour
{
	/* A single "strength" modifier. This encompasses both the power and distance of transmission.
	 * For now, merely specifies distance, but later the algorithm should be tweaked appropriately.
	 */
	public float strength = 100;

	void Start()
	{

	}

	void Update()
	{
		var influenceSphere = transform.root.Find("influenceBubble");

		if(influenceSphere)
			influenceSphere.transform.localScale = 2*(new Vector3(strength, strength, strength));
	}

	// Returns the power at the given point of the influence bubble.
	public float Power(Vector3 pos)
	{
		float distance = Vector3.Distance(this.transform.root.position, pos);

		return -distance + strength;
	}
}

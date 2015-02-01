using UnityEngine;
using System.Collections;

public class Loyal : MonoBehaviour
{
	public Influential Allegiance { get { return allegiance; }}
	public bool Feral { get { return (allegiance == null ? true : false); }}

	public Influential allegiance;
	public float loyalty = 0;
	public int duration = 0;

	void Start()
	{

	}

	void Update()
	{
		if(allegiance) {
			// Update loyalty.
			loyalty = TimeModifier()*allegiance.Power(transform.root.position);

			if(loyalty < 0) {
				// Check for other queens.
				Influential[] queens = (Influential[])this.transform.root.GetComponentsInParent(typeof(Influential));

				if(queens.Length >= 1) {
					// Reassign to new queen.
					Influential newQueen = null;
					float power = 0f;

					foreach(Influential heir in queens) {
						float tempPower = heir.Power(transform.root.position);

						if(tempPower > power) {
							newQueen = heir;
							power = tempPower;
						}
					}

					allegiance = newQueen;
				} else {
					allegiance = null;
				}
			}
		} else {
			// Feral!
		}
	}

	protected float TimeModifier()
	{
		float value = 0.5f*duration*duration;

		if(value > 1.0) return 1.0f;
		else return value;
	}
}

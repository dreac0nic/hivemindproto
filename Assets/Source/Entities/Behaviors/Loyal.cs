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
			float currModie = 1.0f;
			float sumOfOtherQueens = 0f;

			Influential[] queens = Object.FindObjectsOfType(typeof(Influential)) as Influential[];

			foreach(Influential queen in queens) {
				float power = queen.Power(transform.root.position);

				if(power > 0) sumOfOtherQueens += power;
			}

			loyalty = currModie*allegiance.Power(transform.root.position) - 0*sumOfOtherQueens;

			if(loyalty < 0)
					allegiance = null;
		} else {
			// Feral!
			allegiance = GetStrongestQueen();
		}
	}

	protected Influential GetStrongestQueen()
	{
		Influential[] queens = Object.FindObjectsOfType(typeof(Influential)) as Influential[];
		Influential queen = null;
		float power = 0f;

		foreach(Influential heir in queens) {
			float tempPower = heir.Power(transform.root.position);

			if(tempPower > power) {
				queen = heir;
				power = tempPower;
			}
		}

		return queen;
	}

	protected float TimeModifier()
	{
		float value = 0.5f*duration*duration;

		if(value > 1.0) return 1.0f;
		else return value;
	}
}

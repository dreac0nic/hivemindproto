using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using HiveMind;

public class Loyal : MonoBehaviour
{
	public Influential Allegiance { get { return allegiance; }}
	public bool Feral { get { return (allegiance == null ? true : false); }}

	public Influential allegiance;
	public float loyalty = 0;
	public int duration = 0;

	private Renderer renderer;

	public bool CheckLoyalty(Influential playerQueen)
	{
		Influential controllingQueen = GetComponent<Influential>();
		Movable action = GetComponent<Movable>();
		return (allegiance == playerQueen) || (controllingQueen && controllingQueen == playerQueen);
	}

	void Start()
	{
		renderer = GetComponentInChildren<Renderer>();
	}

	void Update()
	{
		if(allegiance) {
			renderer.material = allegiance.Player.color;

			// Update loyalty.
			float sumOfOtherQueens = 0f;

			HashSet<Influential> queens = Influential.Queens;

			foreach(Influential queen in queens) {
				float power = queen.Power(transform.position);

				if(power > 0) sumOfOtherQueens += power;
			}

			loyalty = allegiance.Power(transform.position) - 0*sumOfOtherQueens;

			if(loyalty < 0)
					allegiance = null;
		} else {
			renderer.material = Options.O.FeralColor;

			// Feral!
			allegiance = GetStrongestQueen();
		}

	}

	protected Influential GetStrongestQueen()
	{
		HashSet<Influential> queens = Influential.Queens;
		Influential queen = null;
		float power = Options.O.FeralMinConvertPower;

		foreach(Influential heir in queens) {
			float tempPower = heir.Power(transform.position);

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

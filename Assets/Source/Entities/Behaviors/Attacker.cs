using UnityEngine;

public class Attacker : MonoBehaviour
{
	public int AttackDamage;
	public int AttackRate;
	public float AttackRange;

	private GameObject otherUnit;
	private UnitStats unitStats;
	private Movable movable;

	void Start()
	{
		unitStats = GetComponent<UnitStats>();
		movable = GetComponent<Movable>();
	}

	public void StartAttacking(GameObject otherUnit)
	{
		this.otherUnit = otherUnit;
		InvokeRepeating("AttackUnit", 0, AttackRate);
	}

	public void StopAttacking()
	{
		CancelInvoke("AttackUnit");
	}

	public void AttackUnit()
	{
		UnitStats otherStats = otherUnit.GetComponent<UnitStats>();
		float distanceToOtherUnit = Vector3.Distance(otherStats.Position, unitStats.Position);
		if (distanceToOtherUnit <= AttackRange)
		{
			otherStats.Health -= AttackDamage;
		}
		else
		{
			movable.Follow(otherUnit, AttackRange);
		}
	}
}

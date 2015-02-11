using UnityEngine;

public class Attacker : MonoBehaviour
{
	public int AttackDamage;
	public int AttackRate;
	public float AttackRange;

	private GameObject otherUnit;

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
		float distanceToOtherUnit = Vector3.Distance(otherUnit.GetComponent<UnitStats>().Position, GetComponent<UnitStats>().Position);
		if (distanceToOtherUnit <= AttackRange)
		{
			otherUnit.GetComponent<UnitStats>().Health -= AttackDamage;
		}
		else
		{
			GetComponent<Movable>().Follow(otherUnit, AttackRange);
		}
	}
}

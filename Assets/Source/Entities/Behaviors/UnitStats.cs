using UnityEngine;
using HiveMind;

public class UnitStats : MonoBehaviour 
{
	public int Health;
	public int MaxHealth;
	public float Loyalty;
	public float MaxLoyalty;
	public int Hunger;
	public float Speed;
	public Vector3 Position;

	void Start()
	{
	}

	void Update()
	{
		Position = transform.position;
	}
}

using UnityEngine;

public class UnitStats : MonoBehaviour 
{
	public int Health;
	public float Loyalty;
	public int Hunger;
	public float Speed;
	public Vector3 Position;

	void Update()
	{
		Position = transform.position;
	}
}

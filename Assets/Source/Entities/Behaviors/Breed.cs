using UnityEngine;
using System.Collections;

public class Breed : MonoBehaviour
{
	public GameObject BaseUnitGameObject;
	public float BirthingRate;
	public bool IsBirthing;
	public float BirthRadius = 10f;
	public Vector3 birthingPosition = new Vector3();
	public Quaternion birthingRotation = new Quaternion();

	public void StartBirthing()
	{
		birthingPosition = new Vector3(Random.value, 0, Random.value);
		birthingPosition.Normalize();
		birthingPosition = birthingPosition * BirthRadius + transform.position;

		InvokeRepeating("GiveBirth", 0, BirthingRate);
		IsBirthing = true;
	}

	public void StopBirthing()
	{
		CancelInvoke("GiveBirth");
		IsBirthing = false;
	}

	private void GiveBirth()
	{
		Instantiate(BaseUnitGameObject, birthingPosition, birthingRotation);
	}
}

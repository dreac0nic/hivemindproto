using UnityEngine;
using System.Collections;

public class Breed : MonoBehaviour
{
	public GameObject BaseUnitGameObject;
	public float BirthingRate;
	public bool IsBirthing;
	public Vector3 birthingPosition = new Vector3();
	public Quaternion birthingRotation = new Quaternion();

	public void StartBirthing()
	{
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

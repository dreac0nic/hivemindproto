using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
	public Vector3 destination = Vector3.zero;
	private NavMeshAgent navAgent;
	private bool updated;
	private bool following;
	private GameObject unitFollowing;

	void Start()
	{
		navAgent = GetComponent<NavMeshAgent>();
		updated = true;
	}

	void Update()
	{
		if(!updated) {
			navAgent.SetDestination(destination);
			updated = true;
		}

		if (following)
		{
			navAgent.SetDestination(unitFollowing.transform.position);
		}
	}

	public void Move(Vector3 destination, float stoppingDistance = 0)
	{
		navAgent.stoppingDistance = stoppingDistance;
		this.destination = destination;
		updated = false;
		following = false;
	}

	public void Follow(GameObject otherUnit, float stoppingDistance = 0)
	{
		navAgent.stoppingDistance = stoppingDistance;
		unitFollowing = otherUnit;
		following = true;
	}
}

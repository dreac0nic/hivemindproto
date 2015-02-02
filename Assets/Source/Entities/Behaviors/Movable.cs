using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
	private Vector3 destination = Vector3.zero;
	private NavMeshAgent navAgent;
	private bool updated;

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
	}

	public void Move(Vector3 destination)
	{
		this.destination = destination;
		updated = false;
	}
}

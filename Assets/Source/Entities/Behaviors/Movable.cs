using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
	private Vector3 destination = Vector3.zero;
	private NavMeshAgent navAgent;
	private bool updated;
	private bool following;
	private GameObject unitFollowing;
	private Animator animator;

	void Start()
	{
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();

		updated = true;
	}

	void Update()
	{
		if(!animator) {Debug.Log("Failedness on " + this.name);}

		if(!updated) {
			navAgent.SetDestination(destination);

			updated = true;
		}

		if (following)
			navAgent.SetDestination(unitFollowing.transform.position);

		if(animator)
		{
			if(navAgent.remainingDistance > navAgent.stoppingDistance) {
				animator.SetBool("Walk", true);
			}
			else {
				animator.SetBool("Walk", false);
			}
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

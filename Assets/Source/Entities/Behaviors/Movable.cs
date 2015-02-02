using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
	public Vector3 destination = Vector3.zero;

	private Animator animator;
	private NavMeshAgent navAgent;

	void Start()
	{
		animator = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		navAgent.SetDestination(destination);
	}
}

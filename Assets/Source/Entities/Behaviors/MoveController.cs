﻿using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
	public bool walking = false;
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
		animator.SetBool("Walk", walking);
		navAgent.SetDestination(destination);
	}
}

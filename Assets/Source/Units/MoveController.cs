using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	public bool walking = false;

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update () {
		animator.SetBool("Walk", walking);
	}
}

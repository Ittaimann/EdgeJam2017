using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;
    private PlayerMovement pm;
    
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("IsMoving", pm._isMoving());
        animator.SetBool("IsGrounded", pm._isGrounded());
        animator.SetBool("IsDashing", pm.IsDashing());
	}
}

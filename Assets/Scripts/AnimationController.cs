using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;
    private PlayerMovement pm;
    private AttackController ac;
    private bool IsMoving;
    private bool IsJumping;
    private bool IsAttacking;



	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMovement>();
        ac = GetComponent<AttackController>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("IsMoving", pm._isMoving());
        animator.SetBool("IsGrounded", pm._isGrounded());
        animator.SetBool("IsAttacking", ac.IsAttacking());
        animator.SetBool("IsDashing", ac.IsDashing());
	}
}

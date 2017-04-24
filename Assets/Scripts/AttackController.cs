using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public bool isAttacking;
    public bool isDashing;
    public bool finishedDashing;
    public bool isInvincible;
    public float dashSpeed;
    public Collider2D attackCollider;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        attackCollider = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Dash") && !isDashing)
        {
            Dash();
        }
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void EnableInvincible()
    {
        isInvincible = true;
    }

    public void DisableInvincible()
    {
        isInvincible = false;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void Dash()
    {
        isDashing = true;
    }

    public void FinishDashMovement()
    {
        finishedDashing = true;
    }

    public void FinishedDashing()
    {
        isDashing = false;
        finishedDashing = false;
    }

    public void Attack()
    {
        isAttacking = true;
    }

    public void FinishedAttacking()
    {
        isAttacking = true;
    }

    public void EnableCollider()
    {
        attackCollider.gameObject.SetActive(true);
    }

    public void DisableCollider()
    {
        attackCollider.gameObject.SetActive(false);
    }

}

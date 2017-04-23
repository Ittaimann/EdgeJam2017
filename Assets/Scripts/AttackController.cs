using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    private bool isAttacking;
    private bool isInvincible;
    public Collider2D attackCollider;

    // Use this for initialization
    void Start()
    {
        attackCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {

    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsInvincible()
    {
        return isInvincible;
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

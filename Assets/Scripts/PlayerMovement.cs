using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public float minJumpVelocity = 2.5f;
    public float maxJumpVelocity = 5.0f;

    public int maxJumpCount = 2;

    private bool isDashing = false;
    public float dashSpeed;
    public float dashDistance;

    private Rigidbody2D rigid;
    private bool jumpPressed = false;
    private bool jumpCancel = false;
    private bool isGrounded = false;
    private bool isMoving = false;
    private int jumpCount = 2;
    [HideInInspector]
    public SpriteRenderer sprite;
    private Animator anim;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount;
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _MoveInput();
        _JumpInputCheck();
        _DashInputCheck();
        _UpdateAnimation();
    }
    private void _UpdateAnimation()
    {
        anim.SetBool("IsMoving", _isMoving());
        anim.SetBool("IsGrounded", _isGrounded());
        anim.SetBool("IsDashing", IsDashing());
    }


    void FixedUpdate()
    {
        if (_isGrounded())
            jumpCount = maxJumpCount;
        _Jump();
        _Dash();
    }

    private void _MoveInput()
    {
        if (!isDashing)
        {
            float horizontal = Input.GetAxis("Horizontal");
            isMoving = (horizontal != 0);
            sprite.flipX = _FlipFace(horizontal);
            rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);
        }

    }
    private bool _FlipFace(float horizontal)
    {

        if (horizontal < 0)
            return false;
        if (horizontal == 0)
            return sprite.flipX;
        return true;
    }
    private void _JumpInputCheck()
    {
        if (Input.GetButtonDown("Jump") && !jumpPressed && jumpCount != 0)
            jumpPressed = true;
        if (Input.GetButtonUp("Jump") && !_isGrounded())
            jumpCancel = true;
    }

    private void _DashInputCheck()
    {
        if (Input.GetButtonDown("Dash") && !isDashing)
            isDashing = true;
    }

    private void _Jump()
    {
        if (jumpPressed && jumpCount > 0)
        {
            anim.Play("mc jump",-1,0f);
            jumpCount--;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.velocity += new Vector2(0, maxJumpVelocity);
            jumpPressed = false;
        }

        if (jumpCancel)
        {
            if (rigid.velocity.y > minJumpVelocity)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.velocity += new Vector2(0, minJumpVelocity);
            }
            jumpCancel = false;
        }

    }
    public bool _isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-.1f), Vector2.down);
        if (!ray)
            return false;
        if (ray.collider.CompareTag("Ground") && ray.distance <= 0.1f)
            return true;
        return false;
    }

    public bool _isMoving()
    {
        return isMoving;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    private void _Dash()
    {
        if (isDashing)
            StartCoroutine(_DashRoutine());
    }
    private IEnumerator _DashRoutine()
    {
        if (sprite.flipX)
            transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + new Vector2(dashDistance, 0), dashDistance / dashSpeed);
        else
            transform.position = Vector2.Lerp(transform.position, ((Vector2)transform.position - new Vector2(dashDistance, 0)), dashDistance / dashSpeed);
        yield return new WaitForSeconds(dashDistance / dashSpeed);
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

}
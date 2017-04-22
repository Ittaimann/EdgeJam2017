using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5.0f;
    public float minJumpVelocity = 2.5f;
    public float maxJumpVelocity = 5.0f;
    
    public int maxJumpCount = 2;
    
    private Rigidbody2D rigid;
    private bool jump = false;
    private bool jumpCancel = false;
    private bool isGrounded = false;
    private int jumpCount = 2;
    [HideInInspector]
    public static SpriteRenderer sprite;

    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount;
        sprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () {
        _MoveInput();
        _JumpInputCheck();
    }
    void FixedUpdate()
    {
        _Jump();
    }

    private void _MoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        sprite.flipX = _FlipFace(horizontal);
        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);
    }
    private bool _FlipFace(float horizontal)
    {
        if (horizontal > 0)
            return false;
        if (horizontal == 0)
            return sprite.flipX;
        return true;
    }
    private void _JumpInputCheck()
    {
        if (Input.GetButtonDown("Jump") && !jump)
            jump = true;
        if (Input.GetButtonUp("Jump") && !_isGrounded())
            jumpCancel = true;
    }
    private void _Jump()
    {
        if (jump)
        {
            if (jumpCount > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.velocity += new Vector2(0, maxJumpVelocity);
                jumpCount--;
            }
            if (_isGrounded())
                jump = false;
        }
        else
            jumpCount = maxJumpCount;
        if (jumpCancel)
        {
            if(rigid.velocity.y > minJumpVelocity)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.velocity += new Vector2(0, minJumpVelocity);
            }
            jumpCancel = false;
        }
    }
    private bool _isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .55f), Vector2.down);
        if (!ray)
            return false;
        if (ray.collider.CompareTag("Ground") && ray.distance <= 0)
            return true;
        return false;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public float minJumpVelocity = 2.5f;
    public float maxJumpVelocity = 5.0f;

    public int maxJumpCount = 2;

    private Rigidbody2D rigid;
    private bool jumpPressed = false;
    private bool jumpCancel = false;
    private bool isGrounded = false;
    [SerializeField]
    private int jumpCount = 2;
    [HideInInspector]
    public SpriteRenderer sprite;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount;
        sprite = GetComponentInChildren<SpriteRenderer>();

    }

    void Update()
    {

        _MoveInput();
        _JumpInputCheck();
    }
    void FixedUpdate()
    {
        if (_isGrounded())
            jumpCount = maxJumpCount;
        _Jump();

    }

    public void CopyState(PlayerMovement other)
    {
        this.jumpCount = other.jumpCount;
        this.jumpPressed = other.jumpPressed;
        this.jumpCancel = other.jumpCancel;
        this.isGrounded = other.isGrounded;
        this._Jump();
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
        if (Input.GetButtonDown("Jump") && !jumpPressed &&jumpCount!=0)
            jumpPressed = true;
        if (Input.GetButtonUp("Jump") && !_isGrounded())
            jumpCancel = true;
    }
    private void _Jump()
    {
        if (jumpPressed && jumpCount > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.velocity += new Vector2(0, maxJumpVelocity);
            jumpCount--;
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
    private bool _isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .55f), Vector2.down);
        if (!ray)
            return false;
        if (ray.collider.CompareTag("Ground") && ray.distance <= 0)
            return true;
        return false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    public enum FALLDIRECTION
    {
        up,
        down,
        left,
        right
    };
    public FALLDIRECTION falldirection;
    public float fallSpeed;


    public float gracePeriod;
    private Rigidbody2D rb;
    private float currentTimer;
    private Vector2 startingPosition;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.transform.position.y > transform.position.y)
            {
                StartCoroutine(FallCoroutine());
            }
        }
    }

    private IEnumerator FallCoroutine()
    {
        yield return new WaitForSeconds(gracePeriod);
        Fall();
        yield return new WaitForSeconds(6);
        Respawn();
    }

    private void Fall()
    {
        if(falldirection == FALLDIRECTION.down)
        {
            rb.velocity = new Vector2(0, fallSpeed * -1);
        }
        else if(falldirection == FALLDIRECTION.left)
        {
            rb.velocity = new Vector2(fallSpeed * -1, 0);
        }
        else if(falldirection == FALLDIRECTION.right)
        {
            rb.velocity = new Vector2(fallSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, fallSpeed);
        }
    }

    private void Respawn()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = startingPosition;

    }
}

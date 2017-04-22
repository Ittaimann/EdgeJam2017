using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {

    private Rigidbody2D RB2D;
    public float speed = 5.0f;
    private int dir = 1;
    public Vector2 point;
    private Vector2 orig;
    public static SpriteRenderer sprite;


    // Use this for initialization
    void Start () {
        RB2D =GetComponent<Rigidbody2D>();
        orig = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //_move();
        var i = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector2.Lerp(orig, point, i);
        _flip();
   
    }
    private void _flip()
    {
        if ((Mathf.Round(RB2D.position.x * 10f) / 10f) == (Mathf.Round(point.x * 10f) / 10f))
        {
            sprite.flipX = true;
        }
        if ((Mathf.Round(RB2D.position.x * 10f) / 10f) == (Mathf.Round(orig.x * 10f) / 10f))
        {
            sprite.flipX = false;
        }
    }
}

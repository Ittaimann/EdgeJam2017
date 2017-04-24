using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {

    private Rigidbody2D RB2D;
    public float speed = 5.0f;
    public float chase_speed = .05f;
    private int dir = 1;
    private Vector2 orig;

    public Vector2 point;
    public static SpriteRenderer sprite;
    private GameObject player;

    public int playerdist=4;
    private bool returning;
    private float timer = 0;

    private bool direction;


    // Use this for initialization
    void Start () {
        RB2D =GetComponent<Rigidbody2D>();
        orig = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        returning = false;


    }
	
	// Update is called once per frame
	void Update () {
        Vector2 player_pos = player.transform.position;
        if ((Mathf.Abs((player_pos - RB2D.position).x) < playerdist && Mathf.Abs(player_pos.y-RB2D.position.y)<1))
        {
            RB2D.position = Vector2.MoveTowards(new Vector2(RB2D.position.x,orig.y),new Vector2( player_pos.x,orig.y),chase_speed);
            returning = true;
            if (player_pos.x - RB2D.position.x < 0)
            {
                sprite.flipX = false;
                direction = true;
            }
            else
            {
                sprite.flipX = true;
                direction = false;
            }
        }
        else
        {

            _basicMove();
        }
    
    }

    private void _basicMove()
    {
        if (returning)
        {
            RB2D.position = Vector2.MoveTowards(RB2D.position, orig, chase_speed);

            if (RB2D.position.x < orig.x)
                sprite.flipX = true;
            else
                sprite.flipX = false;

            if ((Mathf.Round(RB2D.position.x * 10f) / 10f)== Mathf.Round(orig.x * 10f) / 10f)
            {
                returning = false;
                timer = 0;
            }

            if ((Mathf.Round(RB2D.position.x * 10f) / 10f) == Mathf.Round(point.x * 10f) / 10f)
            {
                returning = false;
                timer = 1;
            }
        }
        else
        {
            timer += Time.deltaTime;
            var i = Mathf.PingPong(timer, 1);
            RB2D.position = Vector2.Lerp(orig, point, i);
            _basicflip();
        }
    }
    private void _basicflip()
    {


        if (Mathf.PingPong(timer, 1) > .99f)
        {
            sprite.flipX = (point.x < orig.x);
        }
        if ((Mathf.Round(RB2D.position.x * 10f) / 10f) == (Mathf.Round(orig.x * 10f) / 10f))
        {
            sprite.flipX = !(point.x < orig.x);
        }
    }
}

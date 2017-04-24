using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ai : MonoBehaviour {

    private Rigidbody2D RB;
    private Vector2 origin;
    private static SpriteRenderer sprite;
    private GameObject player;
    private bool returning;
    private float timer = 0;



    public float dist;
    public float speed = 5.0f;
    public int playerdist = 4;
    public float chase_speed;
    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody2D>();
        origin = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        returning = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 player_pos = player.transform.position;
        if ((Mathf.Abs((player_pos - RB.position).x) < playerdist && Mathf.Abs(player_pos.y - RB.position.y) < 1))
        {
            RB.position = Vector2.MoveTowards(new Vector2(RB.position.x, origin.y), new Vector2(player_pos.x, origin.y), chase_speed);
            returning = true;
            if (player_pos.x - RB.position.x < 0)
            {
                sprite.flipX = false;

            }
            else
            {
                sprite.flipX = true;
            }
        }
        else {
            _move();

            }

    }

    private void _move()
    {
        print(origin.x - Mathf.Abs(RB.position.x)>dist);
       if (returning)
       {
            sprite.flipX = (RB.position.x < origin.x);

            RB.position = Vector2.MoveTowards(RB.position, origin, chase_speed);
            if ((Mathf.Round(RB.position.x * 10f) / 10f) == Mathf.Round(origin.x * 10f) / 10f)
            {
                returning = false;
                sprite.flipX = true;
                timer = 0;
            }
        }
       else
        {
            timer += Time.deltaTime;
            var i = Mathf.PingPong(timer, 1);
            print(i);
            RB.position = Vector2.Lerp(origin, new Vector2(origin.x+dist,RB.position.y), i);
            _basicflip();

        }

    }

    private void _basicflip()
    {


        if (Mathf.PingPong(timer, 1) > .99f)
        {
            sprite.flipX = (RB.position.x==origin.x);
        }
        if(Mathf.PingPong(timer,1)<.01f)
            sprite.flipX = !(RB.position.x == origin.x+dist);


    }
}

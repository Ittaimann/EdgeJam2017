using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ai : MonoBehaviour {
    private Rigidbody2D RB2D;
    public float speed = 5.0f;
    public float chase_speed = .05f;
    private int dir = 1;
    private Vector2 orig;

    public Vector2 point;
    public static SpriteRenderer sprite;
    private GameObject player;

    public int playerdist = 4;
    private bool returning;
    private float timer = 0;

    private bool direction;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

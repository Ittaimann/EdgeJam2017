using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeController : MonoBehaviour {
    private Camera originCam;
    public PlayerMovement attachedPlayer;

    public EdgeCollider2D leftCollider;
    public EdgeCollider2D rightCollider;
    public EdgeCollider2D topCollider;
    public EdgeCollider2D bottomCollider;
	
	void Start () {
        originCam = this.GetComponent<Camera>();
        attachedPlayer = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

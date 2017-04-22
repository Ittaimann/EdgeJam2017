using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WarpTo
{
    public WarpTo(CameraEdgeController newCam, EdgeCheck newEdge)
    {
        cam = newCam;
        edge = newEdge;
    }
    public CameraEdgeController cam;
    public EdgeCheck edge;
}

public class CameraEdgeController : MonoBehaviour {
    private Camera originCam;
    public PlayerMovement attachedPlayer;
    public GameObject EdgePrefab;

    public CameraEdgeController leftWarp;
    public GameManager.Direction leftWarpSide;
    public CameraEdgeController rightWarp;
    public GameManager.Direction rightWarpSide;
    public CameraEdgeController topWarp;
    public GameManager.Direction topWarpSide;
    public CameraEdgeController bottomWarp;
    public GameManager.Direction bottomWarpSide;

    private EdgeCollider2D leftCollider;
    private EdgeCheck leftEdge;
    private EdgeCollider2D rightCollider;
    private EdgeCheck rightEdge;
    private EdgeCollider2D topCollider;
    private EdgeCheck topEdge;
    private EdgeCollider2D bottomCollider;
    private EdgeCheck bottomEdge;
	
	void Start () {
        originCam = this.GetComponent<Camera>();
        attachedPlayer = FindObjectOfType<PlayerMovement>();

        Debug.Log("Generating Colliders");
        GenerateColliders();
        SetColliders();
	}

    void GenerateColliders()
    {
        Debug.Log("Generating Colliders");
        leftCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        leftCollider.name = "Left";
        leftEdge = leftCollider.GetComponent<EdgeCheck>();

        rightCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        rightCollider.name = "Right";
        rightEdge = rightCollider.GetComponent<EdgeCheck>();

        topCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        topCollider.name = "Top";
        topEdge = topCollider.GetComponent<EdgeCheck>();

        bottomCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        bottomCollider.name = "Bottom";
        bottomEdge = bottomCollider.GetComponent<EdgeCheck>();
    }
	
    void SetColliders()
    {
        float screenRatio = originCam.aspect;
        float yOff = originCam.orthographicSize;
        float xOff = yOff * screenRatio;

        //leftCollider
        leftCollider.points = new Vector2[]{new Vector2(0, yOff),
                                            new Vector2(0, -yOff) };
        leftCollider.transform.localPosition = new Vector2(-xOff, 0);
        //rightCollider
        rightCollider.points = new Vector2[]{new Vector2(0, yOff),
                                             new Vector2(0, -yOff) };
        rightCollider.transform.localPosition = new Vector2(xOff, 0);
        //topCollider
        topCollider.points = new Vector2[]{new Vector2(-xOff, 0),
                                            new Vector2(xOff, 0) };
        topCollider.transform.localPosition = new Vector2(0, yOff);
        //bottomCollider
        bottomCollider.points = new Vector2[]{new Vector2(-xOff, 0),
                                            new Vector2(xOff, 0) };
        bottomCollider.transform.localPosition = new Vector2(0, -yOff);
    }

    public EdgeCheck getEdge(GameManager.Direction dir)
    {
        switch(dir)
        {
            case (GameManager.Direction.UP):
                return topEdge;
            case (GameManager.Direction.DOWN):
                return bottomEdge;
            case (GameManager.Direction.LEFT):
                return leftEdge;
            default: //GameManager.Direction.RIGHT
                return rightEdge;
        }
    }

    public WarpTo getWarp(GameManager.Direction dir)
    {
        switch(dir)
        {
            case (GameManager.Direction.UP):
                return new WarpTo(topWarp, topWarp.getEdge(topWarpSide));
            case (GameManager.Direction.DOWN):
                return new WarpTo(bottomWarp, bottomWarp.getEdge(bottomWarpSide));
            case (GameManager.Direction.LEFT):
                return new WarpTo(leftWarp, leftWarp.getEdge(leftWarpSide));
            default: //GameManager.Direction.RIGHT
                return new WarpTo(rightWarp, rightWarp.getEdge(rightWarpSide));
        }
    }
}

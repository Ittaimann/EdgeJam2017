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

public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public enum Corner
{
    TOP_LEFT,
    TOP_RIGHT,
    BOTTOM_LEFT,
    BOTTOM_RIGHT
}

public class CameraEdgeController : MonoBehaviour {
    private Camera originCam;
    public Corner screenCorner;
    public PlayerMovement attachedPlayer;
    public GameObject EdgePrefab;

    public CameraEdgeController leftWarp;
    public Direction leftWarpSide = Direction.RIGHT;
    public CameraEdgeController rightWarp;
    public Direction rightWarpSide = Direction.LEFT;
    public CameraEdgeController topWarp;
    public Direction topWarpSide = Direction.DOWN;
    public CameraEdgeController bottomWarp;
    public Direction bottomWarpSide = Direction.UP;

    private EdgeCollider2D leftCollider;
    private EdgeCheck leftEdge;
    private EdgeCollider2D rightCollider;
    private EdgeCheck rightEdge;
    private EdgeCollider2D topCollider;
    private EdgeCheck topEdge;
    private EdgeCollider2D bottomCollider;
    private EdgeCheck bottomEdge;
	
	void Start () {
        if(ClonePool.instance != null)
        {
            ClonePool.instance.registerCamera(this);
        }

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
        leftEdge.dir = Direction.LEFT;

        rightCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        rightCollider.name = "Right";
        rightEdge = rightCollider.GetComponent<EdgeCheck>();
        rightEdge.dir = Direction.RIGHT;

        topCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        topCollider.name = "Top";
        topEdge = topCollider.GetComponent<EdgeCheck>();
        topEdge.dir = Direction.UP;

        bottomCollider = Instantiate(EdgePrefab, this.transform, false).GetComponent<EdgeCollider2D>();
        bottomCollider.name = "Bottom";
        bottomEdge = bottomCollider.GetComponent<EdgeCheck>();
        bottomEdge.dir = Direction.DOWN;
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

    public EdgeCheck getEdge(Direction dir)
    {
        switch(dir)
        {
            case (Direction.UP):
                return topEdge;
            case (Direction.DOWN):
                return bottomEdge;
            case (Direction.LEFT):
                return leftEdge;
            default: //Direction.RIGHT
                return rightEdge;
        }
    }

    public WarpTo getWarp(Direction dir)
    {
        switch(dir)
        {
            case (Direction.UP):
                return new WarpTo(topWarp, topWarp.getEdge(topWarpSide));
            case (Direction.DOWN):
                return new WarpTo(bottomWarp, bottomWarp.getEdge(bottomWarpSide));
            case (Direction.LEFT):
                return new WarpTo(leftWarp, leftWarp.getEdge(leftWarpSide));
            default: //Direction.RIGHT
                return new WarpTo(rightWarp, rightWarp.getEdge(rightWarpSide));
        }
    }
}

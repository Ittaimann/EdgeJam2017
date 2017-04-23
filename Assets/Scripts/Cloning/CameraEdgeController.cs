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
    public bool useCustomEdges;
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
    public EdgeCheck leftEdge;
    private EdgeCollider2D rightCollider;
    public EdgeCheck rightEdge;
    private EdgeCollider2D topCollider;
    public EdgeCheck topEdge;
    private EdgeCollider2D bottomCollider;
    public EdgeCheck bottomEdge;

    void Awake()
    {
        originCam = this.GetComponent<Camera>();
    }

    void Start () {
        if(ClonePool.instance != null)
        {
            ClonePool.instance.registerCamera(this);
        }

        attachedPlayer = FindObjectOfType<PlayerMovement>();

        GenerateColliders();
        SetColliders();
	}

    void GenerateColliders()
    {
        if (useCustomEdges)
        {
            leftEdge.dir = Direction.LEFT;
            leftCollider = leftEdge.GetComponent<EdgeCollider2D>();
            rightEdge.dir = Direction.RIGHT;
            rightCollider = rightEdge.GetComponent<EdgeCollider2D>();
            topEdge.dir = Direction.UP;
            topCollider = topEdge.GetComponent<EdgeCollider2D>();
            bottomEdge.dir = Direction.DOWN;
            bottomCollider = bottomEdge.GetComponent<EdgeCollider2D>();
        }
        else
        {
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

    public bool containsPoint(Vector2 point)
    {
        //returns if a point exists within its othnographic render box
        Vector2 boxsize = new Vector2((originCam.orthographicSize * 2) * originCam.aspect,
                                originCam.orthographicSize * 2);
        Rect bounds = new Rect( (Vector2)this.transform.position - boxsize/2, boxsize);
        return point.x >= bounds.xMin && point.x <= bounds.xMax && point.y >= bounds.yMin && point.y <= bounds.yMax;
    }
}

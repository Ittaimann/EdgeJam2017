using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour {
    public enum sawDirection
    {
        Up,
        Left,
        Down,
        Right,
        UpLeft,
        DownLeft,
        DownRight,
        UpRight
    }

    public enum sawState
    {
        toEnd,
        toStart
    }

    public sawDirection movementDirection;
    public float distance;
    public float moveTime = 2;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private sawState currentState = sawState.toEnd;
    private float timeSpent;

	// Use this for initialization
	void Start () {
        SetPoints();
        timeSpent = 0;
	}

    void FixedUpdate()
    {
        MoveSaw();
        timeSpent += Time.fixedDeltaTime;
    }

    void MoveSaw()
    {
        switch (currentState)
        {
            case (sawState.toEnd):
                this.transform.position = Vector2.Lerp(startPoint, endPoint, timeSpent / moveTime);
                if (timeSpent >= moveTime) { currentState = sawState.toStart; timeSpent = 0; }
                break;
            default: //case (sawState.toStart)
                this.transform.position = Vector2.Lerp(endPoint, startPoint, timeSpent / moveTime);
                if (timeSpent >= moveTime) { currentState = sawState.toEnd; timeSpent = 0; }
                break;
        }
    }

    void SetPoints()
    {
        startPoint = this.transform.position;

        switch(movementDirection)
        {
            case (sawDirection.Up):
                endPoint = Vector2.up * distance;
                break;
            case (sawDirection.Left):
                endPoint = Vector2.left * distance;
                break;
            case (sawDirection.Down):
                endPoint = Vector2.down * distance;
                break;
            case (sawDirection.Right):
                endPoint = Vector2.right * distance;
                break;
            case (sawDirection.UpLeft):
                endPoint = (new Vector2(-1, 1)).normalized * distance;
                break;
            case (sawDirection.UpRight):
                endPoint = (new Vector2(1, 1)).normalized * distance;
                break;
            case (sawDirection.DownRight):
                endPoint = (new Vector2(1, -1)).normalized * distance;
                break;
            default: // case (sawDirection.DownLeft):
                endPoint = (new Vector2(-1, -1)).normalized * distance;
                break;
        }
        endPoint += startPoint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EdgeCheck : MonoBehaviour {
    private CameraEdgeController cameraParent;
    public Direction dir;

	void Start ()
    {
        cameraParent = GetComponentInParent<CameraEdgeController>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(ClonePool.instance != null && other.tag == "Player")
        {
            // ClonePool.instance.TriggerSpawnEvent(cameraParent, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(ClonePool.instance != null && other.tag == "Player")
        {
            ClonePool.instance.TriggerDespawnEvent(cameraParent, this);
        }
    }
}

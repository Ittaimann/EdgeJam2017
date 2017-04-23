using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EdgeCheck : MonoBehaviour {
    private CameraEdgeController cameraParent;
    public GameManager.Direction dir;

	void Start () {
        cameraParent = GetComponentInParent<CameraEdgeController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // ClonePool.instance.TriggerSpawnEvent(cameraParent, this);
        }
    }
}

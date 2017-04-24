using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePool : MonoBehaviour {
    public static ClonePool instance;

    public PlayerMovement PlayerPrefab;
    public bool UseWarp = false;

    private PlayerMovement[] PlayerPool; //Not safe to walk in
    private Dictionary<CameraEdgeController, PlayerMovement> CameraPlayerMap;
    private Dictionary<Corner, CameraEdgeController> cornerMap;
    public PlayerMovement MainPlayer;

    void Awake()
    {
        instance = this;
        CameraPlayerMap = new Dictionary<CameraEdgeController, PlayerMovement>();
        cornerMap = new Dictionary<Corner, CameraEdgeController>();

        
    }

    public void registerCamera(CameraEdgeController cam)
    {
        cornerMap.Add(cam.screenCorner, cam);
        if(cornerMap.Count >= 4)
        {
            ScanForMainPlayer();
            GeneratePlayerClones();
        }
    }

    private void ScanForMainPlayer()
    {
        //Attaches the main player to its respective camera if existing in scene
        foreach(CameraEdgeController cam in cornerMap.Values)
        {
            if(cam.containsPoint(MainPlayer.transform.position))
            {
                CameraPlayerMap.Add(cam, MainPlayer);
                cam.attachedPlayer = MainPlayer;
                break;
            }
        }
    }

    private void GeneratePlayerClones()
    {
        foreach(CameraEdgeController cam in cornerMap.Values)
        {
            if(!CameraPlayerMap.ContainsKey(cam))
            {
                CameraPlayerMap.Add(cam, Instantiate<PlayerMovement>(PlayerPrefab, this.transform));
                CameraPlayerMap[cam].gameObject.SetActive(false);
                cam.attachedPlayer = CameraPlayerMap[cam];
            }
        }
    }

    public void TriggerSpawnEvent (CameraEdgeController alerted, EdgeCheck edge) //Gets called when this camera's edge collider detects the player
    {
        if (UseWarp)
        {
            WarpTo destination = alerted.getWarp(edge.dir);
            if (!CameraPlayerMap[destination.cam].gameObject.activeInHierarchy) //If no other camera is currently using this player
            {
                PlayerMovement Clone = CameraPlayerMap[destination.cam];
                PlayerMovement Source = CameraPlayerMap[alerted];
                Vector3 offset = edge.transform.position - Source.transform.position;
                Clone.transform.position = destination.edge.transform.position - offset;
                Clone.gameObject.SetActive(true);
                Clone.GetComponent<Rigidbody2D>().velocity = Source.GetComponent<Rigidbody2D>().velocity;
                //TODO: Keep player variables consistent
                //Clone.CopyState(Source);
            }
        }
    }

    public void TriggerDespawnEvent (CameraEdgeController alerted, EdgeCheck edge)
    {
        if (UseWarp)
        {
            if (true && !alerted.containsPoint(alerted.attachedPlayer.transform.position)) //TODO: If no other camera edge controls this player
            {
                CameraPlayerMap[alerted].gameObject.SetActive(false);
            }
        }
    }
}
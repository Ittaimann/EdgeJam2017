using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeshoot : MonoBehaviour {
    public enum SpikeState {
        ready,
        launching,
        resting,
        returning
    }
    public enum SpikeDir
    {
        up,
        down,
        left,
        right
    }

    private SpikeState activeState;

    public SpikeDir spikeDir;
    public float dist;
    public float launchTime;
    public float restTime;
    public float returnTime;
    
    private float timeSpent;

    private Spike_trigger trigger;
    private Vector2 Startpoint;
    private Vector2 Endpoint;

    private bool isShooting = false;
    // Use this for initialization

    void Start()
    {
        activeState = SpikeState.ready;
        Startpoint = transform.position;
        trigger = GetComponentInChildren<Spike_trigger>();
    }
    void FixedUpdate()
    {
        if (isShooting)
        {
            timeSpent += Time.deltaTime;
            switch (activeState)
            {
                case (SpikeState.launching):
                    transform.position = Vector2.Lerp(Startpoint, Endpoint, timeSpent / launchTime);
                    if(timeSpent>=launchTime)
                    {
                        activeState = SpikeState.resting;
                    }
                    break;
                case (SpikeState.resting):
                    if (timeSpent >= launchTime+restTime)
                    {
                        activeState = SpikeState.returning;
                    }
                    break;
                case (SpikeState.returning):
                    transform.position = Vector2.Lerp(Endpoint, Startpoint,( timeSpent-(launchTime + restTime))/returnTime);
                    if (timeSpent >= launchTime + restTime + returnTime)
                    {
                        activeState = SpikeState.ready;
                       trigger.gameObject.SetActive(true);
                        isShooting = false;
                    }
                    break;
            }
        }           
    }

    public void shoot()
    {
        GetComponentInChildren<Spike_trigger>().gameObject.SetActive(false);
        timeSpent = 0;
        switch (spikeDir)
        {
            case SpikeDir.down:
                Endpoint = Vector2.down * dist;
                break;
            case SpikeDir.up:
                Endpoint = Vector2.up * dist;
                break;
            case SpikeDir.left:
                Endpoint = Vector2.left * dist;
                break;
            case SpikeDir.right:
                Endpoint = Vector2.right * dist;
                break;
        }
        Endpoint += Startpoint;

        isShooting = true;
        activeState = SpikeState.launching;
    }
}

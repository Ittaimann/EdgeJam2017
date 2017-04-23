using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spike_trigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            GetComponentInParent<spikeshoot>().shoot();
    }
}

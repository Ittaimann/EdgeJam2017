using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_kill : MonoBehaviour {
    private float time;
    // Use this for initialization
    void FixedUpdate()
    {
        var i = Time.deltaTime;
        time += i;
        print(time);
        if (time >= 2.5f)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    private static Music _instance;

    public static Music Instance { get { return _instance; } }
    // Use this for initialization
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}

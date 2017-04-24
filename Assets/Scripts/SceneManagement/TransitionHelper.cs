using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHelper : MonoBehaviour {
    //Stores name of level for the transition scene to use
    private static object _lock = new object();
    private static TransitionHelper _instance;
    public static TransitionHelper instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<TransitionHelper>("Singletons/TransitionHelper");

                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }
    }

    public virtual void OnApplicationQuit()
    {
        _instance = null;
    }

    public string levelName;
    public string sceneName;
}

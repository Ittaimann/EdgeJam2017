using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour {
    public enum FadeState
    {
        LoadIn,
        Rest,
        LoadOut
    }

    public FadeState fadeState = FadeState.Rest;
    public float fadeTime = 2;

    private SceneController sc;
    private Material MaskMaterial;
    private float timeLeft = 0;
    private string targetScene;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SceneController>();
        MaskMaterial = GetComponent<Image>().material;
        timeLeft = fadeTime;
        if(fadeState == FadeState.Rest)
        {
            MaskMaterial.SetFloat("_Cutoff", 1);
        }
	}

    public void Transition (string scene)
    {
        targetScene = scene;
        timeLeft = fadeTime;
        fadeState = FadeState.LoadOut;
    }
	
	// Update is called once per frame
	void Update () {
		switch(fadeState)
        {
            case (FadeState.LoadIn):
                timeLeft -= Time.deltaTime;
                MaskMaterial.SetFloat("_Cutoff", 1 - (timeLeft / fadeTime));
                if(timeLeft <= 0)
                {
                    fadeState = FadeState.Rest;
                }
                break;
            case (FadeState.LoadOut):
                timeLeft -= Time.deltaTime;
                MaskMaterial.SetFloat("_Cutoff", timeLeft / fadeTime);
                if(timeLeft <= 0)
                {
                    sc.swapToSceneTransition(targetScene);
                }
                break; 
        }
	}
}

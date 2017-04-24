using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontFader : MonoBehaviour {
    public enum FadeState
    {
        FadeIn,
        Rest,
        FadeOut,
        Blank
    }
    public float fadeTime = 1;
    public float restTime = 2.5f;
    public SceneController sc;
    public string targetScene;

    private FadeState fadeState;
    private Text fadeText;
    private float timeLeft;

	void Start () {

        fadeText = this.GetComponent<Text>();
        fadeState = FadeState.FadeIn;
        timeLeft = fadeTime;

        targetScene = TransitionHelper.instance.sceneName;
        fadeText.text = TransitionHelper.instance.levelName;
	}
	
	// Update is called once per frame
	void Update () {

        timeLeft -= Time.deltaTime;

		switch(fadeState)
        {
            case (FadeState.FadeIn):

                changeAlpha(1-(timeLeft / fadeTime));
                if(timeLeft <= 0)
                {
                    fadeState = FadeState.Rest;
                    timeLeft = restTime;
                }
                break;

            case (FadeState.Rest):
    
                if(timeLeft <= 0)
                {
                    fadeState = FadeState.FadeOut;
                    timeLeft = fadeTime;
                }
                break;

            case (FadeState.FadeOut):

                changeAlpha(timeLeft / fadeTime);
                if(timeLeft <= 0)
                {
                    fadeState = FadeState.Blank;
                    sc.swapToScene(targetScene);
                }
                break;
        }
	}

    private void changeAlpha(float alpha)
    {
        Color c = fadeText.color;
        fadeText.color = new Color(c.r, c.g, c.b, alpha);
    }
}

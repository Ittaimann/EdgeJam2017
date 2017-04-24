using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public void swapToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void swapToSceneTransition(string scene)
    {
        TransitionHelper.instance.levelName = scene;
        TransitionHelper.instance.sceneName = scene;
        SceneManager.LoadScene(scene);
    }

    public void swapToSceneDelayed(string scene, float time)
    {
        StartCoroutine(ExecuteSceneSwap(scene, time));
    }

    private IEnumerator ExecuteSceneSwap (string scene, float time)
    {
        yield return new WaitForSeconds(time);
        swapToScene(scene);
    }
}

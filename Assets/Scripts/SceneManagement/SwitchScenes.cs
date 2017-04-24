using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScenes : MonoBehaviour
{
    public SceneFade fader;
    public string scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            fader.Transition(scene);
    }
}

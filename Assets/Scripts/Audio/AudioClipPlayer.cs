using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour {

    public string clipName; // This clip should exist in the Audio Manager prefab

    void Start()
    {
        AudioManager.instance.playAudio(clipName);
    }
}

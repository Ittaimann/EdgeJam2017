using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public struct ClipVolumePair
    {
        public ClipVolumePair (AudioClip newAudio, float newVolume)
        {
            clip = newAudio;
            volume = newVolume;
        }

        public AudioClip clip;
        public float volume;
    }

    public AudioClip mainMenu;
    public float mainMenuVolume = 1f;
    public AudioClip levelMusic;
    public float levelMusicVolume = 0.06f;

    private AudioSource source;

    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            return _instance;
        }
    }

    public void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            source = this.GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void playAudio(string sound )
    {
        ClipVolumePair target;
        switch(sound)
        {
            case ("MainMenu"):
                target = new ClipVolumePair(mainMenu, mainMenuVolume);
                break;
            default: //levelmusic
                target = new ClipVolumePair(levelMusic, levelMusicVolume);
                break;
        }

        if (source.clip != null && target.clip != source.clip)
        {
            source.Stop();
            source.clip = target.clip;
            source.volume = target.volume;
            source.Play();
        }

    }

}

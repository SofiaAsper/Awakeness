using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // public static AudioManager instance;

    private void Awake()
    {
        // if (instance == null) instance = this;
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        // DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.mixerGroup;
            // set the rolloff mode
            s.source.rolloffMode = AudioRolloffMode.Logarithmic;
            s.source.spatialBlend = 1;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.maxDistance = s.maxDistance;
            s.source.spatialBlend = s.spatialBlend;
        }
    }
    void Start()
    {
        Play("Fire");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            // Debug.LogWarning("Sound: " + name + " not found!");s
            return;
        }
        if (!s.source.isPlaying)
        {
            StopAllAudio();
            s.source.Play();
        }

    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            // Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void StopAllAudio()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void OnMouseEnter()
    {
        Play("Click");
    }

}

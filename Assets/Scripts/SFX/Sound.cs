
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    public enum RolloffMode
    {
        logarithmic, linear, custom
    }
    public string name;

    public AudioClip clip;
    public AudioMixerGroup mixerGroup;

    public RolloffMode rfMode;

    [Range(0f, 1f)]
    public float volume = .5f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(.1f, 3f)]
    public float spatialBlend = 1f;

    public bool loop;

    public bool playOnAwake;

    public float maxDistance;

    [HideInInspector]
    public AudioSource source;


}

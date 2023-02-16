using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatformAudio : MonoBehaviour
{
    public RotationPlatform platform;
    public AudioSource source;

    public SoundData Rotate;
    void Start()
    {
        platform.OnRotate += () => source.PlayOneShot(Rotate.Clip, Rotate.Volume);
    }
}

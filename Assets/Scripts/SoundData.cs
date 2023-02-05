using System;
using UnityEngine;

[Serializable]
public class SoundData
{
    public AudioClip[] clips;
    public Vector2 volumeRange = new Vector2(1, 1);

    public AudioClip Clip => clips[UnityEngine.Random.Range(0, clips.Length)];
    public float Volume => UnityEngine.Random.Range(volumeRange.x, volumeRange.y);
}

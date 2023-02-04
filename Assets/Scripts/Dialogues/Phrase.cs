using System;
using UnityEngine;

[Serializable]
public class Phrase
{
    public AudioClip clip;
    [TextArea] public string dialogText;

    AudioSource source;

    public bool IsEnded => source.time >= clip.length;

    public void Play(AudioSource source)
    {
        this.source = source;

        UnityEngine.Object.FindObjectOfType<UIController>().StartDialog(dialogText);
        source.clip = clip;
        source.Play();
    }

    public void Stop()
    {
        UnityEngine.Object.FindObjectOfType<UIController>().StopDialog();
        source.Stop();
    }
}

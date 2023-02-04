using UnityEngine;

public class Phrase
{
    AudioSource source;
    PhraseData data;

    public Phrase(AudioSource source)
    {
        this.source = source;
    }

    public bool IsEnded => source.time >= data.clip.length;

    public void Play(PhraseData data)
    {
        this.data = data;

        UnityEngine.Object.FindObjectOfType<UIController>().StartDialog(data.dialogText);
        source.clip = data.clip;
        source.Play();
    }

    public void Stop()
    {
        UnityEngine.Object.FindObjectOfType<UIController>().StopDialog();
        source.Stop();
    }
}

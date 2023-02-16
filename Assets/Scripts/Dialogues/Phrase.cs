using UnityEngine;

public class Phrase
{
    public AudioSource source;
    PhraseData data;

    UIController controller;

    public Phrase(AudioSource source)
    {
        this.source = source;
        controller = Object.FindObjectOfType<UIController>();
    }

    public bool IsEnded { 
        get
        {
            var boolean = source.time >= data.clip.length;
            if (boolean) controller.StopDialog();
            return boolean;
        }
    }

    public void Play(PhraseData data)
    {
        this.data = data;

        controller.StartDialog(data.dialogText);
        source.clip = data.clip;
        source.Play();
    }

    public void Stop()
    {
        controller.StopDialog();
        source.Stop();
    }
    public bool IsPaused { get; set; }
    public void Pause()
    {
        source.Pause();
        IsPaused = true;
    }
    public void Resume()
    {
        source.UnPause();
        IsPaused = false;
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PressAudio : MonoBehaviour
{
    public Press press;
    public AudioSource Source;
    public AudioSource LiftSource;

    public SoundData Fall;
    public SoundData LiftEnd;

    public float FadeDuration;
    private void Start()
    {
        press.OnFall += () => PlayClip(Fall);
        press.OnLiftStart += StartPlayLift;
        press.OnLiftEnd += StopPlayLift;
    }

    void PlayClip(SoundData data) => Source.PlayOneShot(data.Clip, data.Volume);

    private void StartPlayLift()
    {
        LiftSource.DOFade(1, FadeDuration).From(0);
        LiftSource.Play();
    }
    private void StopPlayLift()
    {
        LiftSource.DOFade(0, FadeDuration).From(1);
        PlayClip(LiftEnd);
    }
}

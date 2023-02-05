using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    public AudioClip start, exit;
    public AudioSource source;

    public void NewGame()
    {
        StopAllCoroutines();
        StartCoroutine(WaitForClip(start, () => SceneManager.LoadScene(1)));
    }
    public void Exit() 
    {
        StopAllCoroutines();
        StartCoroutine(WaitForClip(exit, Application.Quit));
    }

    IEnumerator WaitForClip(AudioClip clip, Action a)
    {
        source.clip = clip;
        source.Play();
        yield return new WaitUntil(() => source.time >= clip.length);
        a();
    }
}

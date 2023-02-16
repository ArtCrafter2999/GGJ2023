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
    private bool _isDisabled = false; 

    public void NewGame()
    {
        if(!_isDisabled)
            StartCoroutine(WaitForClip(start, () => SceneManager.LoadScene(1)));
    }
    public void Exit() 
    {
        if (!_isDisabled)
            StartCoroutine(WaitForClip(exit, Application.Quit));
    }

    IEnumerator WaitForClip(AudioClip clip, Action a)
    {
        _isDisabled = true;
        source.clip = clip;
        source.Play();
        yield return new WaitUntil(() => source.time >= clip.length);
        _isDisabled = false;
        a();
        
    }
}

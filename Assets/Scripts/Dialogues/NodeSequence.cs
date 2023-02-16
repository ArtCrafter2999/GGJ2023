using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;
using System.Collections;

public class NodeSequence : MonoBehaviour
{
    [System.ComponentModel.ReadOnly(true)]
    public int CurrentIndex;

    public List<NodeBase> Nodes;
    public AudioSource source;

    private bool _isSkipped = false;

    public bool PlayOnAwake;
    public bool IsPlaying { get; private set; }
    public bool IsPaused { get; private set; }

    private bool _anySkipCause => _isSkipped || IsPaused || !IsPlaying;
    void Start()
    {
        GameManager.Instance.Controlls.UI.SkipDialog.performed += c => SkipNode();
        GameManager.Instance.Controlls.UI.Pause.performed += c => { 
            if(IsPaused) ResumeSequence(); 
            else PauseSequence(); 
        };
        foreach (var node in Nodes)
        {
            node.NodeSequence = this;
        }
        if (PlayOnAwake) StartSequence();
    }

    public void StartSequence()
    {
        StartCoroutine(StartSequenceWork());
    }
    public void StopSequence()
    {
        IsPlaying = false;
    }
    public void PauseSequence()
    {
        IsPaused = true;
    }
    public void ResumeSequence()
    {
        IsPaused = false;
    }
    public void SkipNode()
    {
        _isSkipped = Nodes[CurrentIndex].Skip();
    }

    private IEnumerator StartSequenceWork()
    {
        IsPlaying = true;
        for (int i = 0; i < Nodes.Count; i++)
        {
            CurrentIndex = i;
            Nodes[i].Init();
            yield return new WaitUntil(() => Nodes[i].Invoke() || _anySkipCause);
            if (!IsPlaying)
            {
                Nodes[i].Stop();
                break;
            }
            if (IsPaused)
            {
                Nodes[i].Pause();
                i--;
                yield return new WaitWhile(() => IsPaused);
            }
            if (_anySkipCause) Nodes[i].Break();
            _isSkipped = false;
        }
        StopSequence();
    }
}
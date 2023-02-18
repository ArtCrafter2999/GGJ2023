using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/PhraseNode")]
public class PhraseNode : NodeBase
{
    public PhraseData data;

    private Phrase _phrase;
    public override bool Invoke()
    {
        //NodeSequence.print($"name: {_phrase.source.clip.name},    time: {_phrase.source.time},    lenght: {_phrase.source.clip.length},    IsEnded: {_phrase.IsEnded}");
        
        return _phrase.IsEnded || !_phrase.source.isPlaying;
    }

    public override void Init()
    {
        if (_phrase != null && _phrase.IsPaused)
        {
            _phrase.Resume();
        }
        else
        {
            _phrase = new Phrase(NodeSequence.source);
            _phrase.Play(data);
        }
    }
    public override void Pause()
    {
        _phrase.Pause();
    }
    public override void Break()
    {
        if(!_phrase.IsPaused)
        {
            _phrase.Stop();
            _phrase = null;
        }
    }
    public override bool Skip()
    {
        return true;
    }
}


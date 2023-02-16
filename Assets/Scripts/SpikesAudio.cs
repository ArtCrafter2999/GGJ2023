using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesAudio : MonoBehaviour
{
    public TouchDamage Spikes;

    public AudioSource source;

    public SoundData Sound;
    private bool _canPlay;
    void Start()
    {
        Spikes.OnTouched += () => 
        {
            if (_canPlay)
            {
                source.PlayOneShot(Sound.Clip, Sound.Volume);
                _canPlay = false;
            }
        };
    }
    private void Update()
    {
        if (!_canPlay) _canPlay = !GameManager.Instance.PlayerComponent.health.IsDead;
    }
}

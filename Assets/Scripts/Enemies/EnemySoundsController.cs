using DG.Tweening;
using System;
using UnityEngine;

public class EnemySoundsController : MonoBehaviour
{
    public AudioSource source;
    public AudioSource walkSource;

    public SoundData shot;
    public SoundData growth;
    public SoundData hit;
    public SoundData walk;

    public float moveSoundInterval;

    public float walkFadeDuration;

    private Player _player => GameManager.Instance.PlayerComponent;
    Enemy enemy;

    float moveTick;
    bool wasMoving;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        _player.growther.DidGrowth += OnGrowth;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
            _player.growther.DidGrowth -= OnGrowth;
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = enemy._isMoving;
        if (!wasMoving && moving)
        {
            StartWalkClip();
        }
        if (wasMoving && !moving)
        {
            StopWalkClip();
        }

        wasMoving = moving;
    }

    private void OnGrowth()
    {
        PlayClip(growth);
    }

    void PlayClip(SoundData data) => source.PlayOneShot(data.Clip, data.Volume);

    void StartWalkClip()
    {
        walkSource.clip = walk.Clip;
        walkSource.DOFade(walk.Volume, walkFadeDuration).From(0);
        walkSource.Play();
    }
    void StopWalkClip()
    {
        walkSource.DOFade(0, walkFadeDuration).From(walk.Volume).OnComplete(walkSource.Stop);
    }

    public void PlayShot() => PlayClip(shot);
    public void PlayHit() => PlayClip(hit);
}

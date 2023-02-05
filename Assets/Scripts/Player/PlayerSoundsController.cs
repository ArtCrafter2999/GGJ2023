using DG.Tweening;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    public AudioSource source;
    public AudioSource walkSource;
    public AudioSource idleSource;

    public PlayerController Controller;
    public Health Health;

    public SoundData walk;
    public SoundData jump;
    public SoundData doubleJump;
    public SoundData drop;
    public SoundData idle;
    public SoundData death;
    public SoundData stick;

    public float moveSoundInterval;
    public float idleSoundInterval;

    public float walkFadeDuration;

    float moveTick, idleTick;
    bool wasMoving, wasIdle;

    // Start is called before the first frame update
    void Start()
    {
        idleTick = idleSoundInterval;

        Controller.Jumped += () => PlayClip(jump);
        Controller.DoubleJumped += () => PlayClip(doubleJump);
        Controller.Dropped += () => PlayClip(drop);
        Controller.Grabbed += () => PlayClip(stick);
        Health.OnDeath += () => PlayClip(death);
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = !Mathf.Approximately(Controller.MoveInput, 0f) && Controller.IsOnGround;
        if (!wasMoving && moving)
        {
            StartWalkClip();
        }
        if (wasMoving && !moving)
        {
            StopWalkClip();
        }

        bool idle = !(Mathf.Approximately(Controller.MoveInput, 0f) && !Controller.IsGrabbing && !Controller.IsSliding);
        if (!wasIdle && idle)
        {
            idleTick = idleSoundInterval;
        }
        if (wasIdle && !idle)
        {
            StopIdleClip();
        }

        idleTick -= Time.deltaTime;
        if (idleTick <= 0)
        {
            StartIdleClip();
            idleTick = idleSoundInterval;
        }

        wasMoving = moving;
        wasIdle = idle;
    }

    void PlayClip(SoundData data) => source.PlayOneShot(data.Clip, data.Volume);
    void StartWalkClip()
    {
        walkSource.DOFade(1, walkFadeDuration).From(0);
        walkSource.Play();
    }
    void StopWalkClip()
    {
        walkSource.DOFade(0, walkFadeDuration).From(1).OnComplete(walkSource.Stop);
    }

    void StartIdleClip()
    {
        idleSource.DOFade(idle.Volume, walkFadeDuration).From(0);
        idleSource.Play();
    }
    void StopIdleClip()
    {
        idleSource.DOFade(0, walkFadeDuration).From(idle.Volume).OnComplete(idleSource.Stop);
    }
}

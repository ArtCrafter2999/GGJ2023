using DG.Tweening;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    public AudioSource source;
    public AudioSource walkSource;
    public AudioSource idleSource;

    public PlayerController Controller;
    public Health Health;

    public AudioClip walk;
    public AudioClip jump;
    public AudioClip doubleJump;
    public AudioClip drop;
    public AudioClip idle;
    public AudioClip death;

    [Range(0f, 1f)] public float jumpVolume;
    [Range(0f, 1f)] public float dropVolume;
    [Range(0f, 1f)] public float idleVolume;
    
    public float moveSoundInterval;
    public float idleSoundInterval;

    public float walkFadeDuration;

    float moveTick, idleTick;
    bool wasMoving, wasIdle;

    // Start is called before the first frame update
    void Start()
    {
        idleTick = idleSoundInterval;

        Controller.Jumped += () => PlayClip(jump, jumpVolume);
        Controller.DoubleJumped += () => PlayClip(doubleJump, jumpVolume);
        Controller.Dropped += () => PlayClip(drop, dropVolume);
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

    void PlayClip(AudioClip clip, float volume = 1) => source.PlayOneShot(clip, volume);
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
        idleSource.DOFade(idleVolume, walkFadeDuration).From(0);
        idleSource.Play();
    }
    void StopIdleClip()
    {
        idleSource.DOFade(0, walkFadeDuration).From(idleVolume).OnComplete(idleSource.Stop);
    }
}

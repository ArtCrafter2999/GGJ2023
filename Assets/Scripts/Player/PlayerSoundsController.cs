using DG.Tweening;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    public AudioSource source;
    public AudioSource walkSource;
    public AudioSource slideSource;
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
    public float slideFadeDuration;

    float moveTick, idleTick;
    bool wasMoving, wasIdle, wasSlide;

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
        bool moving = Controller.IsMoving && Controller.IsOnGround;

        if (wasSlide != Controller.IsGrabbing)
        {
            if (Controller.IsGrabbing) StartClip(slideSource, slideFadeDuration);
            else StopClip(slideSource, slideFadeDuration);
        }

        if (wasMoving != moving)
        {
            if(moving) StartClip(walkSource, walkFadeDuration);
            else StopClip(walkSource, walkFadeDuration);
        }

        bool idle = !Controller.IsMoving && !Controller.IsGrabbing && !Controller.IsSliding;
        if(wasIdle != idle)
        {
            if(idle) idleTick = idleSoundInterval;
            else StopClip(idleSource, walkFadeDuration);
        }

        idleTick -= Time.deltaTime;
        if (idleTick <= 0)
        {
            StartClip(idleSource, walkFadeDuration);
            idleTick = idleSoundInterval;
        }

        wasMoving = moving;
        wasIdle = idle;
        wasSlide = Controller.IsGrabbing;
    }

    void PlayClip(SoundData data) => source.PlayOneShot(data.Clip, data.Volume);

    void StartClip(AudioSource source, float fadeDuration)
    {
        if(source.isPlaying) source.DOKill();
        else source.DOFade(1, fadeDuration).From(0);
        source.Play();
    }
    void StopClip(AudioSource source, float fadeDuration)
    {
        source.DOFade(0, fadeDuration).From(1).OnComplete(source.Stop).OnKill(source.Stop);
    }
}

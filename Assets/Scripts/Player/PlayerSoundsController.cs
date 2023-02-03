using DG.Tweening;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    public AudioSource source;
    public AudioSource walkSource;

    public PlayerController Controller;
    public Health Health;

    public AudioClip walk;
    public AudioClip jump;
    public AudioClip doubleJump;
    public AudioClip drop;
    public AudioClip idle;
    public AudioClip death;
    
    public float moveSoundInterval;
    public float idleSoundInterval;

    public float walkFadeDuration;

    float moveTick, idleTick;
    bool wasMoving;

    // Start is called before the first frame update
    void Start()
    {
        idleTick = idleSoundInterval;

        Controller.Jumped += () => PlayClip(jump);
        Controller.DoubleJumped += () => PlayClip(doubleJump);
        Controller.Dropped += () => PlayClip(drop);
        Health.OnDeath += () => PlayClip(death);
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = !Mathf.Approximately(Controller.MoveInput, 0f) && Controller.GroundCollider;
        if (!wasMoving && moving)
        {
            StartWalkClip();
        }
        if (wasMoving && !moving)
        {
            StopWalkClip();
        }

        if (!Mathf.Approximately(Controller.MoveInput, 0f) || Controller.IsGrabbing || Controller.IsSliding)
        {
            idleTick = idleSoundInterval;
        }

        idleTick -= Time.deltaTime;
        if (idleTick <= 0)
        {
            PlayClip(idle);
            idleTick = idleSoundInterval;
        }
        
        wasMoving = moving;
    }

    void PlayClip(AudioClip clip) => source.PlayOneShot(clip);

    void StartWalkClip()
    {
        walkSource.DOFade(1, walkFadeDuration).From(0);
        walkSource.Play();
    }
    void StopWalkClip()
    {
        walkSource.DOFade(0, walkFadeDuration).From(1).OnComplete(walkSource.Stop);
    }
}

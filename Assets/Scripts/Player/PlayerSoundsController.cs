using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    public AudioSource source;
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

    float moveTick, idleTick;

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
        if (!Mathf.Approximately(Controller.MoveInput, 0f))
        {
            idleTick = idleSoundInterval;

            if (moveTick <= 0 && (Controller.GroundCollider || Controller.IsGrabbing || Controller.IsSliding))
            {
                PlayClip(walk);
                moveTick = moveSoundInterval;
            }
            moveTick -= Time.deltaTime;
        }
        else
        {
            moveTick = 0;

            if (idleTick <= 0)
            {
                PlayClip(idle);
                idleTick = idleSoundInterval;
            }
            idleTick -= Time.deltaTime;
        }
    }

    void PlayClip(AudioClip clip) => source.PlayOneShot(clip);
}

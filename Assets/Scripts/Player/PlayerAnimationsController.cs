using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    public Animator Animator;
    public PlayerController Controller;
    public SpriteRenderer Sprite;
    public Health Health;
    // Start is called before the first frame update
    void Start()
    {
        Controller.PlayerControlls.Player.Jump.performed += ctx => Animator.SetTrigger("Jump");
        Health.OnDeath += () =>
        {
            Sprite.gameObject.transform.parent = Sprite.gameObject.transform.parent.transform.parent;
            Animator.SetBool("IsGrabbing", false);
            Animator.SetTrigger("Death");
        };
    }

    // Update is called once per frame
    void Update()
    {
        Sprite.flipX = Controller.LookingDirection == Direction2.Left;
        Animator.SetBool("IsMoving", Controller.IsMoving);
        Animator.SetBool("IsJumping", Controller.IsJumping);
        Animator.SetBool("IsGrounded", Controller.IsOnGround);
        Animator.SetBool("IsGrabbing", Controller.IsGrabbing);
        Animator.SetBool("IsCeiling", Controller.IsCeiling);
    }
}

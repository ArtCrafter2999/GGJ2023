using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
        Health.OnDeath += () => Animator.SetTrigger("Death");
        Animator.GetBehaviour<PlayerBehaviour>().OnExit.AddListener(() => gameObject.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        Sprite.flipX = Controller.LookingDirection == PlayerController.Direction2.Left;
        Animator.SetFloat("MovingSpeed", Mathf.Abs(Controller.MoveInput));
        Animator.SetBool("IsJumping", Controller.IsJumping);
        Animator.SetBool("IsGrounded", Controller.GroundCollider);
        Animator.SetBool("IsGrabbing", Controller.IsGrabbing);
        Animator.SetBool("IsCeiling", Controller.IsCeiling);
    }
}
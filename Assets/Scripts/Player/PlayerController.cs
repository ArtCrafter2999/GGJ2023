using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("References")]
	private Rigidbody2D rb;
	private Health health;
	private Growther growther;

	[Header("Movement")]
	public float MoveSpeed;
	public float Acceleration;
	public float Decceleration;
	public float VelPower;
	[Space(10)]
	private float _moveInput;
	public float MoveInput { get => _moveInput; set 
		{
			if (_disableMovement == null)
				_moveInput = value;
			else 
			{
				_moveInput = _disableMovement == Direction2.Left ?
					MathF.Max(0, value) :
					Mathf.Min(0, value);
			} 
		} 
	}
	private Direction2? _disableMovement = null;
	[Space(10)]
	public float FrictionAmount;

	[Header("Jump")]
	public float JumpForce;
	public float CoyoteTime;
	private float _coyoteTimeLeft;
	[Range(0, 1)]
	public float jumpCutMultiplier;
	[Space(10)]
	public float FallGravityMultiplier;
	private float _gravityScale;
	[Space(10)]
	public int JumpsCount;
	public bool IsJumping { get; private set; }
    private int _jumpsCountLeft;
	[Header("Grab/Slide")]
	public float GrabTime;
    [Range(0.1f, 1)]
    public float GrabLerp;
	public float SlideSpeed;

    [Header("Checks")]
	public Transform GroundCheckPoint;
	public Transform RightCheckPoint;
	public Transform LeftCheckPoint;
    public Transform CeilingCheckPoint;
    public Vector2 CheckSize;
	[Space(10)]
	public LayerMask GroundLayer;
    public LayerMask WallsLayer;

    public enum Direction2
    {
		Right,
		Left
    }
	public Direction2 LookingDirection { get; private set; } = Direction2.Right;
	[SerializeField]
	private bool _isGrabbing = false;
	public bool IsGrabbing { get => _isGrabbing; private set { if (!_isGrabbing && value) StartCoroutine(LateSlide()); _isGrabbing = value; } }
	public bool IsSliding { get; private set; }
	public bool IsCeiling => CeilingCollider;

    public Collider2D GroundCollider => Physics2D.OverlapBox(GroundCheckPoint.position, Vector2.up * CheckSize, 0, GroundLayer);
	public Collider2D RightCollider => Physics2D.OverlapBox(RightCheckPoint.position, Vector2.right * CheckSize, 0, WallsLayer);
	public Collider2D LeftCollider => Physics2D.OverlapBox(LeftCheckPoint.position, Vector2.right * CheckSize, 0, WallsLayer);
	public Collider2D CeilingCollider => Physics2D.OverlapBox(CeilingCheckPoint.position, Vector2.up * CheckSize, 0, WallsLayer);
    public PlayerControlls PlayerControlls => GameManager.Instance.Controlls;

	public event Action Dropped;
	public event Action Jumped;
	public event Action DoubleJumped;

	private float lastFallSpeed;

    #region Enable / Disable
    private void OnEnable()
	{
		if (GameManager.Instance != null)
			PlayerControlls.Player.Enable();
	}
	private void OnDisable()
	{
        PlayerControlls.Player.Disable();
	}
	#endregion

	private void Start()
	{
        PlayerControlls.Player.Enable();
        PlayerControlls.Player.Jump.performed += ctx => Jump();
		PlayerControlls.Player.Jump.canceled += ctx => OnJumpUp();

		rb = GetComponent<Rigidbody2D>();
		health = GetComponentInChildren<Health>();
		growther = GetComponentInChildren<Growther>();
		health.OnDeath += OnDisable;

		_gravityScale = rb.gravityScale;
	}

    private void Update()
	{
		if (health.IsDead || growther.IsGrowthing) return;

		MoveInput = PlayerControlls.Player.Move.ReadValue<float>();
        if (!IsGrabbing)ChangeDirection();
		GrabCheck();
		GrabWork();

		#region Checks
		if (GroundCollider && !IsJumping) //checks if set box overlaps with ground
		{
			_jumpsCountLeft = JumpsCount;
            _coyoteTimeLeft = CoyoteTime;
		}
		else
		{
            _coyoteTimeLeft -= Time.deltaTime;
		}
		
		if (GroundCollider && lastFallSpeed > 1 && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
			Dropped?.Invoke();
        }

		if (rb.velocity.y < 0.01f)
		{
			IsJumping = false;
		}
		#endregion

		lastFallSpeed = Mathf.Abs(rb.velocity.y);

	}

    #region Grab/Slide
    private void GrabCheck()
	{
		if ((MoveInput > 0 && RightCollider) ||
			(MoveInput < 0 && LeftCollider))
		{
			IsGrabbing = true;
		}
		if (!(RightCollider || LeftCollider) || GroundCollider)
		{
			IsGrabbing = false;
        }
	}

	private void GrabWork()
	{
		if (IsGrabbing)
		{
			if(!IsSliding)rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, GrabLerp);
            _jumpsCountLeft = JumpsCount;
        }
	}
    IEnumerator LateSlide()
    {
        yield return new WaitForSeconds(GrabTime);
        IsSliding = true;
        while (IsGrabbing)
        {
            rb.velocity = new Vector2(0, -SlideSpeed);
            yield return new WaitForFixedUpdate();
        }
        IsSliding = false;
    }
	IEnumerator DisableMovementDirectionForAWhile(Direction2 direction, float disabiltyTime)
	{
		_disableMovement = direction;
		yield return new WaitForSeconds(disabiltyTime);
		_disableMovement = null;
	}
    #endregion

    private void ChangeDirection()
	{
        if (Mathf.Abs(MoveInput) > 0.01f)
        {
            LookingDirection = MoveInput > 0 ? Direction2.Right : Direction2.Left;
		}
	}
	private void FixedUpdate()
	{
		if (health.IsDead || growther.IsGrowthing) return;

		#region Run
		float targetSpeed = MoveInput * MoveSpeed;
		//calculate the direction we want to move in and our desired velocity
		float speedDif = targetSpeed - rb.velocity.x;
		//calculate difference between current velocity and desired velocity
		float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Acceleration : Decceleration;
		//change Acceleration rate depending on situation
		float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, VelPower) * Mathf.Sign(speedDif);
		//applies Acceleration to speed difference, the raises to a set power so Acceleration increases with higher speeds
		//finally multiplies by sign to reapply direction

		rb.AddForce(movement * Vector2.right);
		//applies force force to rigidbody, multiplying by Vector2.right so that it only affects X axis 
		#endregion

		#region Friction
		if (GroundCollider && Mathf.Abs(MoveInput) < 0.01f)
		{
			float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(FrictionAmount));
			amount *= Mathf.Sign(rb.velocity.x);
			rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
		}
		#endregion

		#region Jump Gravity
		if (!IsGrabbing)
		{
			if (rb.velocity.y < -0.01f)
			{
				rb.gravityScale = _gravityScale * FallGravityMultiplier;
			}
			else
			{
				rb.gravityScale = _gravityScale;
			}
		}
		#endregion
	}

	private void Jump()
	{
        if (PlayerControlls.Player.Down.ReadValue<float>() == 1)//Провалитися під платформу
        {
            FallDown();
        }
        else if (IsGrabbing)
		{
			IsGrabbing = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * JumpForce * rb.gravityScale, ForceMode2D.Impulse);
            StartCoroutine(DisableMovementDirectionForAWhile(LookingDirection, 0.1f));
            if (LookingDirection == Direction2.Right)
			{
                rb.AddForce(Vector2.left * JumpForce * rb.gravityScale, ForceMode2D.Impulse);
            }
			else
			{
                rb.AddForce(Vector2.right * JumpForce * rb.gravityScale, ForceMode2D.Impulse);
                
            }
            _coyoteTimeLeft = 0;
			
			Jumped?.Invoke();
			IsJumping = true;
        }
		else if ((_jumpsCountLeft > 0 || _coyoteTimeLeft > 0) || (GroundCollider && !IsJumping) ) //checks if was last grounded within coyoteTime and that jump has been pressed within bufferTime
		{
			_coyoteTimeLeft = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * JumpForce * rb.gravityScale, ForceMode2D.Impulse);

			if (_jumpsCountLeft >= JumpsCount)
            {
				Jumped?.Invoke();
            }
			else
            {
				DoubleJumped?.Invoke();
            }
			IsJumping = true;
		}
		_jumpsCountLeft--;
	}

	public void OnJumpUp()
	{
		if (rb.velocity.y > 0 && IsJumping)
		{
			rb.AddForce(Vector2.down * rb.velocity.y * jumpCutMultiplier, ForceMode2D.Impulse);
		}
	}

	void FallDown()
	{
		if (GroundCollider)
		{
			PlatformEffector2D platform;
			if (GroundCollider.TryGetComponent(out platform))
			{
				
				platform.rotationalOffset = 180;
				StartCoroutine(LateActivatePlatform(platform));
			}
		}
	}

	IEnumerator LateActivatePlatform(PlatformEffector2D platform)
	{
		yield return new WaitForSeconds(0.5f);
		platform.rotationalOffset = 0;
	}
	
}

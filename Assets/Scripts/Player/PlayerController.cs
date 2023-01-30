using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("References")]
	private Rigidbody2D rb;

	[Header("Movement")]
	public float MoveSpeed;
	public float Acceleration;
	public float Decceleration;
	public float VelPower;
	[Space(10)]
	private float _moveInput;
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
    private bool _isJumping;
	public int JumpsCount;
    private int _jumpsCountLeft;
	
	[Header("Checks")]
	public Transform GroundCheckPoint;
	public Vector2 GroundCheckSize;
	[Space(10)]
	public LayerMask GroundLayer;

	public enum Direction2
    {
		Right,
		Left
    }
	public Direction2 LookingDirection { get; private set; } = Direction2.Right; 

	public Collider2D GroundCollider => Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer);

	public PlayerControlls PlayerControlls => GameManager.Instance.Controlls;
	#region Enable / Disable
	private void OnEnable()
	{
		PlayerControlls.Player.Enable();
	}
	private void OnDisable()
	{
        PlayerControlls.Player.Disable();
	}
	#endregion

	private void Start()
	{
		PlayerControlls.Player.Jump.performed += ctx => Jump();
		PlayerControlls.Player.Jump.canceled += ctx => OnJumpUp();

		rb = GetComponent<Rigidbody2D>();

		_gravityScale = rb.gravityScale;
	}

	private void Update()
	{
		_moveInput = PlayerControlls.Player.Move.ReadValue<float>();
        if (Mathf.Abs(_moveInput) > 0.01f)
        {
			LookingDirection = _moveInput > 0 ? Direction2.Right : Direction2.Left;
		}
		#region Checks
		if (GroundCollider && !_isJumping) //checks if set box overlaps with ground
		{
			_jumpsCountLeft = JumpsCount;
            _coyoteTimeLeft = CoyoteTime;
		}
		else
		{
            _coyoteTimeLeft -= Time.deltaTime;
		}

		if (rb.velocity.y < 0)
		{
			_isJumping = false;
		}
		#endregion

	}

	private void FixedUpdate()
	{
		#region Run
		float targetSpeed = _moveInput * MoveSpeed;
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
		if (GroundCollider && Mathf.Abs(_moveInput) < 0.01f)
		{
			float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(FrictionAmount));
			amount *= Mathf.Sign(rb.velocity.x);
			rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
		}
		#endregion

		#region Jump Gravity
		if (rb.velocity.y < 0)
		{
			rb.gravityScale = _gravityScale * FallGravityMultiplier;
		}
		else
		{
			rb.gravityScale = _gravityScale;
		}
		#endregion
	}

	private void Jump()
	{
		if (PlayerControlls.Player.Down.ReadValue<float>() == 1)//Провалитися під платформу
		{
			FallDown();
		}
		else if ((_jumpsCountLeft > 0 && _coyoteTimeLeft > 0) || (GroundCollider && !_isJumping) ) //checks if was last grounded within coyoteTime and that jump has been pressed within bufferTime
		{
			rb.AddForce(Vector2.up * JumpForce * rb.gravityScale, ForceMode2D.Impulse);
			_isJumping = true;
		}
		_jumpsCountLeft--;
	}

	public void OnJumpUp()
	{
		if (rb.velocity.y > 0 && _isJumping)
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

using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    public bool NoAI;
    [Header("Move")]
    public float MoveSpeed;
    public float RestTime = 3f;
    public Transform PatrolingPointRight;
    public Transform PatrolingPointLeft;
    [Header("Detect")]
    public float DetectRangee;
    public LayerMask PlayerLayer;
    [Header("Attack")]
    public float AttackCooldown = 0.5f;
    public GameObject ProjectilePrefab;
    public Transform ShootPointLeft;
    public Transform ShootPointRight;
    [Header("View")]
    public SpriteRenderer Sprite;
    public Animator Animator;
    public Transform PlayerRespawnPoint;
    public bool IsUnderGrowth { get; set; } = false;
    private Direction2 _facing;
    public Direction2 Facing
    {
        get => _facing; set
        {
            _facing = value;
            _currentMovePoint = value == Direction2.Right ? PatrolingPointRight.position : PatrolingPointLeft.position;
            Sprite.flipX = value == Direction2.Left;
        }
    }
    protected Rigidbody2D rb;
    protected Vector3 _currentMovePoint;
    protected bool _isSeePLayer = false;
    public bool _isMoving;
    protected float _cooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!NoAI)
        {
            Facing = Random.Range(0, 2) == 1 ? Direction2.Right : Direction2.Left;

            StartCoroutine(Move());
            StartCoroutine(CheckForPlayer());
        }
        else rb.isKinematic = true;
    }

    void Update()
    {
        Animator.SetBool("IsMoving", _isMoving);
    }
    private IEnumerator Move()
    {
        if (!IsUnderGrowth)
        {
            _isMoving = true;
            while (!_isSeePLayer && _isMoving)
            {
                var dir = Facing == Direction2.Right ? +1 : -1;
                var step = Vector2.right * dir * MoveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + step);
                if (Facing == Direction2.Right && rb.position.x >= _currentMovePoint.x ||
                    Facing == Direction2.Left && rb.position.x <= _currentMovePoint.x)
                {
                    StartCoroutine(SwitchDirection());
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

    }
    private IEnumerator SwitchDirection()
    {
        _isMoving = false;
        var seconds = RestTime;
        while (seconds > 0 && !_isSeePLayer)
        {
            seconds -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (!_isSeePLayer && !IsUnderGrowth)
        {
            Facing = Facing == Direction2.Right ? Direction2.Left : Direction2.Right;
            StartCoroutine(Move());
        }
    }

    private IEnumerator Rest()
    {
        _isMoving = false;
        var seconds = RestTime;
        while (seconds > 0 && !_isSeePLayer)
        {
            seconds -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Move());
    }

    private IEnumerator CheckForPlayer()
    {
        if (!IsUnderGrowth)
        {
            _isSeePLayer = false;
            while (!_isSeePLayer)
            {
                if (RaycastOffset(1) || RaycastOffset(0) || RaycastOffset(-1))
                {
                    _isSeePLayer = true;
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
            Attack();
        }
    }
    private bool RaycastOffset(float offset)
    {
        var hit = Physics2D.Raycast(transform.position + Vector3.up * offset, Facing == Direction2.Right ? Vector3.right : Vector3.left, DetectRangee, PlayerLayer);
        Debug.DrawRay(transform.position + Vector3.up * offset, (Facing == Direction2.Right ? Vector3.right : Vector3.left) * DetectRangee, Color.red, 0.02f);
        return hit && hit.collider != null && !GameManager.Instance.Player.GetComponent<Health>().IsDead;
    }


    public void Death()
    {
        _isMoving = false;
        _isSeePLayer = false;
        IsUnderGrowth = true;
        //GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = true;
        Sprite.transform.parent = transform.parent;
        Sprite.transform.position = Sprite.transform.position + (Vector3.down * 0.01f);
        GameManager.Instance.Player.transform.position = PlayerRespawnPoint.position;
        Bubble.Checkpoint = PlayerRespawnPoint.position;
        Animator.SetTrigger("Death");
    }
    public void FinishDeath()
    {

        Destroy(gameObject);
    }

    //IEnumerator WaitForDeath()
    //{
    //    yield return new WaitForSeconds(1f);

    //    Sprite.transform.parent = transform.parent;
    //    Destroy(gameObject);
    //}

    protected void Attack()
    {
        Animator.SetTrigger("Shoot");
        StartCoroutine(Cooldown());
    }

    protected IEnumerator Cooldown()
    {
        _isMoving = false;
        yield return new WaitForSeconds(AttackCooldown);
        StartCoroutine(CheckForPlayer());
        StartCoroutine(Rest());
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PatrolingPointLeft.position, PatrolingPointRight.position);
    }
}

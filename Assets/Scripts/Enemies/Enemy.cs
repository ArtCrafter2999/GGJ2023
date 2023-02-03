using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Enemy : MonoBehaviour
{
    [Header("Move")]
    public float MoveSpeed;
    public float MoveCooldown = 3f;
    public float PatrolingDistance;
    [Header("Damage")]
    public float Damage = 10f;
    public float DamageCooldown = 0.5f;
    [Header("Checks")]
    public Transform GroundCheckPointRight;
    public Transform GroundCheckPointLeft;
    public Transform WallCheckPointRight;
    public Transform WallCheckPointLeft;
    public Vector2 GroundCheckSize;
    public Vector2 WallCheckSize;
    [Space(10)]
    public LayerMask GroundAndWallLayer;

    public float attackRate;
    public SpriteRenderer renderer;

    public Collider2D RightGroundCollider => Physics2D.OverlapBox(GroundCheckPointRight.position, GroundCheckSize, 0, GroundAndWallLayer);
    public Collider2D LeftGroundCollider => Physics2D.OverlapBox(GroundCheckPointLeft.position, GroundCheckSize, 0, GroundAndWallLayer);
    public Collider2D RightWallCollider => Physics2D.OverlapBox(WallCheckPointRight.position, WallCheckSize, 0, GroundAndWallLayer);
    public Collider2D LeftWallCollider => Physics2D.OverlapBox(WallCheckPointLeft.position, WallCheckSize, 0, GroundAndWallLayer);
    public Health Health => health;

    public bool IsUnderGrowth { get; set; }

    protected bool _right;
    protected Rigidbody2D rb;
    protected Health health;
    protected Vector3 leftMovePoint, rightMovePoint, currentMovePoint;
    protected bool isPatroling = true;
    protected float attackCd;
    protected PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponentInParent<Health>();
        
        _right = Random.Range(0, 2) == 1;
        currentMovePoint = _right ? rightMovePoint : leftMovePoint;
        renderer.flipX = !_right;

        leftMovePoint = transform.position + Vector3.left * (PatrolingDistance / 2);
        rightMovePoint = transform.position + Vector3.right * (PatrolingDistance / 2);
        //StartCoroutine(Walk());

        player = FindObjectOfType<PlayerController>();
    }

    protected virtual void Update()
    {
        if (health.IsDead)
        {
            Death();
        }
        
        if (attackCd > 0)
        {
            attackCd -= Time.deltaTime;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isPatroling)
        {
            var dir = _right ? +1 : -1;
            var step = Vector2.right * dir * MoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + step);
            if (dir > 0 && rb.position.x >= currentMovePoint.x || dir < 0 && rb.position.x <= currentMovePoint.x)
            {
                SwitchDirection();
            }
        }
    }

    void SwitchDirection()
    {
        _right = !_right;
        currentMovePoint = _right ? rightMovePoint : leftMovePoint;
        renderer.flipX = !_right;

    }

    //IEnumerator Walk()
    //{
    //    while (health.IsDead)
    //    {
    //        yield return new WaitForSeconds(MoveCooldown);
    //        yield return new WaitWhile(() => IsUnderGrowth);

    //        while (!(_right ? RightWallCollider : LeftWallCollider) && (_right ? RightGroundCollider : LeftGroundCollider))
    //        {
    //            yield return new WaitForFixedUpdate();
    //            rb.velocity = (MoveSpeed * (_right ? Vector2.right : Vector2.left)) * new Vector2(1, rb.velocity.y);
    //            print("vel: " + rb.velocity);
    //        }
    //        rb.velocity = new Vector2(0, rb.velocity.y);
    //        _right = !_right;
    //    }
    //}

    void Death()
    {
        Destroy(gameObject);
    }

    private bool _canAttack = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colObject = collision.gameObject;
        if (!health.IsDead && _canAttack && !IsUnderGrowth && colObject == GameManager.Instance.Player)
        {
            _canAttack = false;
            StartCoroutine(Cooldown());
        }
    }

    protected void Attack()
    {
        if (attackCd <= 0)
        {
            attackCd = attackRate;
            OnAttack();
        }
    }

    protected abstract void OnAttack();

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(DamageCooldown);
        _canAttack = true;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var leftMovePoint = transform.position + Vector3.left * (PatrolingDistance / 2);
        var rightMovePoint = transform.position + Vector3.right * (PatrolingDistance / 2);
        Gizmos.DrawLine(leftMovePoint, rightMovePoint);
    }
}

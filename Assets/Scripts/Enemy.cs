using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    [Header("Move")]
    public float MoveSpeed;
    public float MoveCooldown = 3f;
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

    public Collider2D RightGroundCollider => Physics2D.OverlapBox(GroundCheckPointRight.position, GroundCheckSize, 0, GroundAndWallLayer);
    public Collider2D LeftGroundCollider => Physics2D.OverlapBox(GroundCheckPointLeft.position, GroundCheckSize, 0, GroundAndWallLayer);
    public Collider2D RightWallCollider => Physics2D.OverlapBox(WallCheckPointRight.position, WallCheckSize, 0, GroundAndWallLayer);
    public Collider2D LeftWallCollider => Physics2D.OverlapBox(WallCheckPointLeft.position, WallCheckSize, 0, GroundAndWallLayer);
    public Health Health => health;

    public bool IsUnderGrowth { get; set; }

    private bool _right;
    private Rigidbody2D rb;
    private Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponentInParent<Health>();
        _right = Random.Range(0, 2) == 1;
        StartCoroutine(Walk());
    }

    private void Update()
    {
        if (health.IsDead)
        {
            Death();
        }
    }

    IEnumerator Walk()
    {
        while (health.IsDead)
        {
            yield return new WaitForSeconds(MoveCooldown);
            yield return new WaitWhile(() => IsUnderGrowth);
            
            while (!(_right ? RightWallCollider : LeftWallCollider) && (_right ? RightGroundCollider : LeftGroundCollider))
            {
                yield return new WaitForFixedUpdate();
                rb.velocity = (MoveSpeed * (_right ? Vector2.right : Vector2.left)) * new Vector2(1, rb.velocity.y);
            }
            rb.velocity = new Vector2(0, rb.velocity.y);
            _right = !_right;
        }
    }

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
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(DamageCooldown);
        _canAttack = true;
    }
}

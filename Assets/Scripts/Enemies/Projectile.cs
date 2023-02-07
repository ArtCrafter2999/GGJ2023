using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    private Direction2 dir;
    public Direction2 Direction
    {
        get => dir; 
        set
        {
            var isLeft = value == Direction2.Left;
            Sprite.flipX = isLeft;
            Collider2D[] cols = GetComponents<Collider2D>();
            foreach (var col in cols)
            {
                col.offset = new Vector2(col.offset.x * (isLeft ? -1 : 1), col.offset.y);
            }
            dir = value;
        }
    }
    public float Speed;
    public SpriteRenderer Sprite;
    public Animator Animator;
    public bool IsDamage = true;

    private bool _isInited = false;
    private bool _isDestroyed = false;

    EnemySoundsController sounds;

    private void Update()
    {
        if (_isInited && !_isDestroyed) transform.position += (Direction == Direction2.Right ? Vector3.right : Vector3.left) * (Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDamage && collision.gameObject == GameManager.Instance.Player)
        {
            GameManager.Instance.Player.GetComponent<Health>().ChangeHealth(-999);
            sounds.PlayHit();
        }
        //else
        //{
        //    if (Animator != null) Animator.SetTrigger("Destroy");
        //    else DestroyEnd();
        //    IsDamage = false;
        //    _isDestroyed = true;
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (IsDamage && collision.gameObject == GameManager.Instance.Player)
        //{
        //    GameManager.Instance.Player.GetComponent<Health>().ChangeHealth(-999);
        //    sounds.PlayHit();
        //}
        //else
            if (Animator != null) Animator.SetTrigger("Destroy");
            else DestroyEnd();
            IsDamage = false;
            _isDestroyed = true;
    }
    public void DestroyEnd()
    {
        sounds.PlayHit();
        Destroy(gameObject);
    }

    public void Init(GameObject enemySource, Direction2 direction)
    {
        this.sounds = enemySource.GetComponentInChildren<EnemySoundsController>();
        Direction = direction;
        _isInited = true;
        sounds.PlayShot();
    }
}


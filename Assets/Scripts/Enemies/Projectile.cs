using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    public Direction2 Direction { get; set; }
    public float Speed;
    public SpriteRenderer Sprite;
    public Animator Animator;
    public bool IsDamage = true;

    private bool _isInited = false;
    private bool _isDestroyed = false;
    private void Update()
    {
        if (_isInited && !_isDestroyed) transform.position += (Direction == Direction2.Right ? Vector3.right : Vector3.left)* (Speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDamage && collision.gameObject == GameManager.Instance.Player)
        {
            GameManager.Instance.Player.GetComponent<Health>().ChangeHealth(-999);
        }
        else
        {
            if (Animator != null) Animator.SetTrigger("Destroy");
            else DestroyEnd();
            IsDamage = false;
            _isDestroyed = true;
        }
    }
    public void DestroyEnd()
    {
        Destroy(gameObject);
    }

    public void Init(Direction2 direction)
    {
        Direction = direction;
        _isInited = true;
    }
}


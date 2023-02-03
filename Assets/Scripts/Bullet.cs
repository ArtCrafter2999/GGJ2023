using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    Tween move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(float speed)
    {
        var step = speed * Time.deltaTime;
        move = rb.DOMoveX(step, Time.deltaTime).SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out var health))
        {
            health.ChangeHealth(-999);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        move.Kill();
    }
}

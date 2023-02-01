using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Growther : MonoBehaviour, IGrowth
{
    public Health health;
    public float restoreHealth;
    public Rigidbody2D rbody;
    public Collider2D col;

    [field: SerializeField] public float GrowthTime { get; set; }

    public bool IsGrowthing { get; private set; }

    public void GrowInTarget(Enemy enemy)
    {
        if (IsGrowthing) return;

        print("grow in : " + enemy.name);
        IsGrowthing = true;
        enemy.IsUnderGrowth = true;
        col.enabled = false;
        rbody.isKinematic = true;
        rbody.velocity = Vector2.zero;
        rbody.DOJump(enemy.transform.position, 2, 1, 0.5f).OnComplete(() => StartCoroutine(DoGrow(enemy)));
    }

    IEnumerator DoGrow(Enemy enemy)
    {
        float time = GrowthTime;
        
        while (time > 0)
        {
            health.ChangeHealth(restoreHealth);

            yield return new WaitForSeconds(1f);
            time -= 1f;
        }
        
        enemy.Health.ChangeHealth(-999);
        IsGrowthing = false;
        enemy.IsUnderGrowth = false;
        rbody.isKinematic = false;
        col.enabled = true;
    }
}

using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Growther : MonoBehaviour, IGrowth
{
    public Health health;
    public float restoreHealth;
    public Rigidbody2D rbody;
    public Collider2D col;

    [field: SerializeField] public float GrowthTime { get; set; }
    private void Start()
    {
        DidGrowth += () => GetComponent<SelfHarmer>().DisableForSeconds(2.5f);
    }
    public bool IsGrowthing { get; set; }

    public event Action DidGrowth;
    public void GrowInTarget(Enemy enemy)
    {
        if (IsGrowthing) return;

        print("grow in : " + enemy.name);
        IsGrowthing = true;
        health.enabled = false;
        enemy.Death();
        enemy.Facing = GameManager.Instance.PlayerController
            .LookingDirection == Direction2.Right ?
            Direction2.Left :
            Direction2.Right;
        DidGrowth.Invoke();
        health.enabled = true;
    }
}

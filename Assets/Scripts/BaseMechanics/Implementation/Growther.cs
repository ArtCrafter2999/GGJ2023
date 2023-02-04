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

    public bool IsGrowthing { get; set; }

    public event Action DidGrowth;
    public void GrowInTarget(Enemy enemy)
    {
        if (IsGrowthing) return;

        print("grow in : " + enemy.name);
        IsGrowthing = true;
        enemy.Death();
        DidGrowth.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedMovePlatform : MovePlatform
{
    [Header("Timer")]
    public float TimeWaiting;

    private float _timeLeft;

    protected override void Start()
    {
        base.Start();
        _timeLeft = TimeWaiting;
    }
    protected override void Update()
    {
        base.Update();
        _timeLeft -= Time.deltaTime;
        if (_timeLeft <= 0)
        {
            StartCoroutine(Move());
            _timeLeft = TimeWaiting + MovingDuration;
        }
    }

}

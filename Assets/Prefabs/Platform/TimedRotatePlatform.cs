using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedRotatePlatform : RotationPlatform
{
    [Header("Timer")]
    public float TimeUp;
    public float TimeDown;
    private float _timeLeft;

    private void Start()
    {
        _timeLeft = TimeUp;
    }
    protected override void Update()
    {
        base.Update();
        _timeLeft -= Time.deltaTime;
        if (_timeLeft <= 0)
        {
            Rotate();
            _timeLeft = _normalState? TimeUp : TimeDown;
        }
    }
}

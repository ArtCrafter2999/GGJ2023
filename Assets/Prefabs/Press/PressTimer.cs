using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTimer : Press
{
    [Header("Timer")]
    public float RepeatTime;
    private float _timeLeft;

    protected override void Start()
    {
        base.Start();
        _timeLeft = RepeatTime + _dropTime;
    }
    protected override void Update()
    {
        base.Update();
        _timeLeft -= Time.deltaTime;
        if (_timeLeft<=0)
        {
            StartCoroutine(Drop());
            _timeLeft = RepeatTime + _dropTime;
        }
    }
}

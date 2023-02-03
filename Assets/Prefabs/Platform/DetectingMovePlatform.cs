using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingMovePlatform : MovePlatform
{
    public float Delay;
    private bool _lastDetectionCollider = false;
    protected override void Update()
    {
        base.Update();
        if (!_lastDetectionCollider && DetectionCollider)
        {
            _lastDetectionCollider = true;
            StartCoroutine(DelayedMove());
        }
        else if (_lastDetectionCollider && !DetectionCollider)
        {
            _lastDetectionCollider = false;
        }


    }
    IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(Delay);
        if (DetectionCollider && !IsMoving)
            StartCoroutine(Move());
    }
}

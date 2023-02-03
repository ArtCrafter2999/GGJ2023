using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    public Animator Animator;
    public PlatformEffector2D Platform;
    protected bool _normalState = true;

    protected virtual void Update()
    {
        Animator.SetBool("NormalState", _normalState);
    }

    public void Rotate()
    {
        Platform.rotationalOffset = _normalState ? 180 : 0;
        _normalState = !_normalState;
    }
}

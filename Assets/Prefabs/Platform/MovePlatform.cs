using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("Moving Platform")]
    public Transform Platform;
    public float MovingDuration;
    public Transform StartPoint { get; set; } = null;
    public Transform EndPoint;
    [Header("Detection")]
    public Transform DetectionPoint;
    public Vector2 DetectionSize;
    public LayerMask DetectionLayers;

    public Collider2D DetectionCollider => Physics2D.OverlapBox(DetectionPoint.position, DetectionSize, 0, DetectionLayers);
    private Transform _memorizeObject = null;
    private Transform _memorizeObjectParent = null;
    public bool OnStart = true;
    public bool IsMoving = false;
    protected virtual void Start()
    {
        StartPoint = new GameObject("StartPoint").transform;
        StartPoint.parent = transform;
        StartPoint.position = transform.position;
    }

    protected virtual void Update()
    {
        if (DetectionCollider)
        {
            _memorizeObject = DetectionCollider.transform;
            if (_memorizeObjectParent == null) _memorizeObjectParent = _memorizeObject.parent;
            _memorizeObject.transform.parent = Platform.transform;
        }
        else if (_memorizeObject != null)
        {
            _memorizeObject.transform.parent = _memorizeObjectParent;
            _memorizeObject = null;
            _memorizeObjectParent = null;
        }
    }

    public IEnumerator Move()
    {
        IsMoving = true;
        float lerp = 0;
        
        while (IsMoving)
        {
            yield return new WaitForFixedUpdate();
            lerp += Time.deltaTime / MovingDuration;
            Platform.transform.position = Vector3.Lerp(
                OnStart ? StartPoint.position : EndPoint.position,
                OnStart ? EndPoint.position : StartPoint.position,
                lerp
            );
            if (lerp >= 1) IsMoving = false;
        }
        OnStart = !OnStart;
    }
}

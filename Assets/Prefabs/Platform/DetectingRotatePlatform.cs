using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingRotatePlatform : RotationPlatform
{
    [Header("Detection")]
    public float Delay;
    public float DownTime;
    public Transform DetectionPoint;
    public Vector2 DetectionSize;
    public LayerMask DetectionLayers;

    private Collider2D _detectionCollider => Physics2D.OverlapBox(DetectionPoint.position, DetectionSize, 0, DetectionLayers);
    private void Start()
    {
        StartCoroutine(Detecting());
    }
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Detecting()
    {
        while (true)
        {
            while (_detectionCollider == null)
            {
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(Delay);
            Rotate();
            yield return new WaitForSeconds(DownTime);
            Rotate();
        }
    }
}

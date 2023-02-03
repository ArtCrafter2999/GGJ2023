using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDetector : Press
{
    [Header("Detection")]
    public float Delay;
    /// <summary>
    /// Затримка після того як прес піднявся до того як він знову почне перевіряти на наявність гравця
    /// </summary>
    public float DetectingDelay;
    public float MaxDetectionDistance;
    public LayerMask DetectionLayers;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(Detecting());
    }
    IEnumerator Detecting()
    {
        Collider2D detected;
        while (true)
        {
            detected = null;
            while (detected == null)
            {
                yield return new WaitForFixedUpdate();
                var hit = Physics2D.Raycast(StopPoint.position, Vector2.down, MaxDetectionDistance, DetectionLayers);
                detected = hit.collider;
                print(hit.collider);
            }

            yield return new WaitForSeconds(Delay);
            StartCoroutine(Drop());
            yield return new WaitForSeconds(DetectingDelay + _dropTime);

        }
    }
}

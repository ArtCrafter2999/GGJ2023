using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    protected float _cableHeight;
    protected float _pressDownEndY;
    protected float _pressDownStartY;
    protected float _dropTime => DownTime + FallingDuration + LiftingDuration;

    [Header("Cable")]
    public Transform Cable;
    public Transform CableStartPoint;
    public Transform CableEndPoint;
    public Transform PressDownPart;
    [Space(10)]
    public Transform StopPoint;
    [Header("Falling/Lifting")]
    public float FallingDuration;
    public float DownTime;
    public float LiftingDuration;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _cableHeight = Cable.GetComponent<SpriteRenderer>().bounds.size.y;
        _pressDownStartY = PressDownPart.transform.position.y;

        float PressDownHeight = PressDownPart.GetComponent<SpriteRenderer>().bounds.size.y;
        RaycastHit2D hit = Physics2D.Raycast(StopPoint.position, Vector2.down);
        _pressDownEndY = hit.point.y + PressDownHeight / 2;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Cable.transform.position = Vector3.Lerp(CableStartPoint.position, CableEndPoint.position, 0.5f);
        Cable.transform.localScale = new Vector3(1,(CableStartPoint.position.y - CableEndPoint.position.y) / _cableHeight + 3, 1);
    }

    public void Fall()
    {
        PressDownPart.transform.DOMoveY(_pressDownEndY, FallingDuration);
    }
    public void Lift()
    {
        PressDownPart.transform.DOMoveY(_pressDownStartY, LiftingDuration);
    }
    public IEnumerator Drop()
    {
        Fall();
        yield return new WaitForSeconds(DownTime);
        Lift();
    }
}

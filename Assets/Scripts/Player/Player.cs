using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController Controller => GameManager.Instance.PlayerController;
    
    public Health health;
    public Growther growther;

    public Transform CameraPoint;
    public Transform AttackPoint;

    public LayerMask EnemyLayer;

    void Start()
    {
        GameManager.Instance.Player = gameObject;
        GameManager.Instance.Controlls.Player.Attack.performed += ctx => Attack();
        GameManager.Instance.Controlls.Player.Interact.performed += ctx => Interact();
        health.OnDeath += () => { enabled = false; };
    }
    private Coroutine _cameraChangePosition;
    private void Update()
    {
        if (health.IsDead || growther.IsGrowthing) return;

        switch (Controller.LookingDirection)
        {
            case Direction2.Right:
                AttackPoint.localPosition = new Vector3(Mathf.Abs(AttackPoint.localPosition.x), AttackPoint.localPosition.y, AttackPoint.localPosition.z);
                if (_cameraChangePosition != null) StopCoroutine(_cameraChangePosition);
                _cameraChangePosition = StartCoroutine(SmoothChangePosition(CameraPoint, new Vector3(1.2f, CameraPoint.localPosition.y, CameraPoint.localPosition.z), 0.05f, true));
                break;
            case Direction2.Left:
                AttackPoint.localPosition = new Vector3(-Mathf.Abs(AttackPoint.localPosition.x), AttackPoint.localPosition.y, AttackPoint.localPosition.z);
                if (_cameraChangePosition != null) StopCoroutine(_cameraChangePosition);
                _cameraChangePosition = StartCoroutine(SmoothChangePosition(CameraPoint, new Vector3(-1.2f, CameraPoint.localPosition.y, CameraPoint.localPosition.z), 0.05f, true));
                break;
        }
    }

    IEnumerator SmoothChangePosition(Transform FromObject, Vector2 ToPosition, float Lerp, bool LocalPosition = false)
    {
        Vector2 FromPosition;
        if (LocalPosition) FromPosition = FromObject.localPosition;
        else FromPosition = FromObject.position;
        for (float i = 0; i < 1f; i+=Lerp)
        {
            if (LocalPosition) FromObject.localPosition = Vector2.Lerp(FromPosition, ToPosition, i);
            else FromObject.position = Vector2.Lerp(FromPosition, ToPosition, i);
            yield return new WaitForFixedUpdate();
        }
        if (LocalPosition)FromObject.localPosition = ToPosition;
        else FromObject.position = ToPosition;
    }
    void Attack()
    {
        // ????? = ?????????? (Growth)
        
        if(!health.IsDead)
        {
            var col = Physics2D.OverlapCircle(AttackPoint.position, Mathf.Abs(AttackPoint.localPosition.x) - 0.6f, EnemyLayer);
            print(col);
            Enemy en = col?.GetComponentInParent<Enemy>();
            if (en)
            {
                growther.GrowInTarget(en);
            }
        }
    }
    void Interact()
    {
        if (Controller.IsOnGround)
        {
            var objects = Physics2D.OverlapCircleAll(transform.position, 3);
            Interactable closest = objects[0].GetComponent<Interactable>();
            foreach (var obj in objects)
            {
                Interactable temp;
                if (obj.TryGetComponent(out temp))
                {
                    var hit = Physics2D.Raycast(transform.position, transform.position - obj.transform.position, 3, Controller.GroundLayer);
                    if (hit.collider == null || hit.collider.gameObject == obj)
                    {
                        if (closest == null || Vector2.Distance(obj.transform.position, transform.position) < Vector2.Distance(closest.transform.position, transform.position))
                            closest = temp;
                    }
                }
            }
            closest?.Interacted.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, Mathf.Abs(AttackPoint.localPosition.x) - 0.6f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerZone : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public bool IsInside { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Instance.Player)
        {
            IsInside = true;
            OnEnter.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Instance.Player)
        {
            IsInside = false;
            OnExit?.Invoke();
        }
    }
}

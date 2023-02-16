using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    public event Action OnTouched;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameManager.Instance.Player)
        {
            OnTouched?.Invoke();
            GameManager.Instance.Player.GetComponent<Health>().ChangeHealth(-999);
        }
    }
}

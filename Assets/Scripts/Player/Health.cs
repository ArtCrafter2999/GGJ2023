using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float Curent;
    public float Maximum;

    // Start is called before the first frame update
    public UnityEvent OnDeath;
    void Start()
    {
        Curent = Maximum;
    }

    public void TakeHealth(float health)
    {
        Curent += health;
        Curent = Mathf.Clamp(Curent, 0, Maximum);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (Curent == 0) OnDeath?.Invoke();
    }
}

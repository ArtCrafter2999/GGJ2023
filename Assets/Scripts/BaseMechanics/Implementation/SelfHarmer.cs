using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SelfHarmer : MonoBehaviour, IHarm
{
    public Health health;
    public Growther growther;

    [field: SerializeField] public float LostHealth { get; set; }

    void OnEnable()
    {
        StartCoroutine(DoLostHealth());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator DoLostHealth()
    {
        while (!health.IsDead)
        {
            yield return new WaitWhile(() => growther.IsGrowthing);
            yield return new WaitForSeconds(1f);
            health.ChangeHealth(-LostHealth);
        }
    }
}

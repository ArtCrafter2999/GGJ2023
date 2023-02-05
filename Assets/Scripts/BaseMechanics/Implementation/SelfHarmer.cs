using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SelfHarmer : MonoBehaviour, IHarm
{
    public Health health;
    public Growther growther;

    [field: SerializeField] public float LostHealth { get; set; }

    private void Update()
    {
        if (!growther.IsGrowthing) health.ChangeHealth(-LostHealth * Time.deltaTime);
        print(!growther.IsGrowthing + " Lost: "+ LostHealth*Time.deltaTime + "    Current: " + health.CurrentHealth);
    }
}

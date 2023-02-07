using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SelfHarmer : MonoBehaviour, IHarm
{
    public Health health;
    public Growther growther;
    public float safeDelay;

    [field: SerializeField] public float LostHealth { get; set; }
    private bool _harmInfoIsShowed = false;

    private void Update()
    {
        if (safeDelay > 0)
        {
            safeDelay -= Time.deltaTime;
            if (safeDelay <= 0 && !_harmInfoIsShowed)
            {
                _harmInfoIsShowed = true;
                FindObjectOfType<UIController>().ShowHarmInfo();
            }
        }
        else
        {
            if (!growther.IsGrowthing) health.ChangeHealth(-LostHealth * Time.deltaTime);
            //print(!growther.IsGrowthing + " Lost: "+ LostHealth*Time.deltaTime + "    Current: " + health.CurrentHealth);
        }
    }
    public void DisableForSeconds(float seconds)
    {
        safeDelay = seconds;
    }
}

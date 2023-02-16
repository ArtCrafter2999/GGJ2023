using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SelfHarmer : MonoBehaviour, IHarm
{
    public Health health;
    public Growther growther;
    public EventNode OnTutorialEnded;
    private float safeDelay;

    [field: SerializeField] public float LostHealth { get; set; }
    private bool tutorial = true;

    private void Start()
    {
        if (OnTutorialEnded != null) OnTutorialEnded.Event.AddListener(() => tutorial = false);
        else tutorial = false;
    }

    private void Update()
    {
        if (safeDelay > 0)
        {
            safeDelay -= Time.deltaTime;
        }
        else if (!tutorial)
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

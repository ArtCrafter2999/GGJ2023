using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image Hpbar;
    public Health Health;
    public UISpritesAnimation Animation;
    public float MaxDuration;
    public float MinDuration;

    private void Update()
    {
        Hpbar.fillAmount = Health.CurrentHealth / Health.MaxHealth;
        Animation.duration = Mathf.Clamp(Hpbar.fillAmount * MaxDuration, MinDuration, MaxDuration);
    }
}

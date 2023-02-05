using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private RectTransform _hpbar;
    private float _hpbarStartWidth; 

    public Health Health;

    private void Start()
    {
        _hpbar = GetComponent<RectTransform>();
        _hpbarStartWidth = _hpbar.localScale.x;
    }

    private void Update()
    {
        _hpbar.localScale = new Vector3(_hpbarStartWidth / Health.MaxHealth * Health.CurrentHealth, _hpbar.localScale.y);
    }
}

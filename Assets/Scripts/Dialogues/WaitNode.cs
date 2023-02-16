using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/WaitNode")]
public class WaitNode : NodeBase
{
    public float Seconds;
    private float _left;
    private bool _isPass;  
    public override bool Invoke()
    {
        return _isPass;
    }
    public override void Init()
    {
        _left = Seconds;
        _isPass = false;
        GameManager.Instance.OnGameTick += OnTick;
    }
    private void OnTick()
    {
        _left-=Time.deltaTime;
        if (_left <= 0) _isPass = true;
    }
}


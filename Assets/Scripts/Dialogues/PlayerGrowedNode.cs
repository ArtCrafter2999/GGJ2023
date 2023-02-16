using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/PlayerGrowedNode")]
public class PlayerGrowedNode : NodeBase
{
    private bool _isGrowed = false;
    public override bool Invoke()
    {
        return _isGrowed;
    }
    public override void Init()
    {
        _isGrowed = false;
        GameManager.Instance.PlayerComponent.growther.DidGrowth += () => _isGrowed = true;
    }
}


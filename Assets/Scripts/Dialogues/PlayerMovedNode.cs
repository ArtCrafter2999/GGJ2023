using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/PlayerMovedNode")]
public class PlayerMovedNode : NodeBase
{
    public override bool Invoke()
    {
        return GameManager.Instance.PlayerController.IsMoving;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/TriggerNode")]
public class TriggerNode : NodeBase
{
    private bool trigger;
    public override bool Invoke()
    {
        return trigger;
    }
    public override void Init()
    {
        trigger = false;
    }
    public void Trigger()
    {
        trigger = true;
    }
}


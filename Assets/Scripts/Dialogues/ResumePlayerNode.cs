using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/ResumePlayerNode")]
public class ResumePlayerNode : NodeBase
{
    public override bool Invoke()
    {
        GameManager.Instance.PlayerComponent.enabled = true;
        GameManager.Instance.PlayerController.enabled = true; 
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GameManager.Instance.Controlls.Player.Enable();
        return true;
    }
}


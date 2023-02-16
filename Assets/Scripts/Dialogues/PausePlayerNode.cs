using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Nodes/PausePlayerNode")]
public class PausePlayerNode : NodeBase
{
    public override bool Invoke()
    {
        GameManager.Instance.PlayerComponent.enabled = false;
        GameManager.Instance.PlayerController.enabled = false;
        GameManager.Instance.Controlls.Player.Disable();
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        GameManager.Instance.PlayerController.MoveInput = 0;
        return true;
    }
}


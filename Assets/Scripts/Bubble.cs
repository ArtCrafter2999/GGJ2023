using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Bubble : MonoBehaviour
{
    public static Vector3 Checkpoint;
    public Dialogue dialogue;
    public LayerMask PlayerLayer;

    bool visited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PlayerLayer)
        {
            Checkpoint = GameManager.Instance.Player.transform.position;
            GameManager.Instance.Player.GetComponentInChildren<SelfHarmer>().enabled = false;
            print("Selfharmer Disabled");

            if (!visited)
            {
                dialogue.PlayDialog();
            }

            visited = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            player.GetComponentInChildren<SelfHarmer>().enabled = true;
            print("Selfharmer Enabled");
        }
    }
}

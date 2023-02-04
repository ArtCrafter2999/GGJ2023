using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Bubble : MonoBehaviour
{
    public static Vector3 Checkpoint;

    public Dialogue dialogue;

    bool visited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            Checkpoint = player.transform.position;
            player.GetComponentInChildren<SelfHarmer>().enabled = false;

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
        }
    }
}

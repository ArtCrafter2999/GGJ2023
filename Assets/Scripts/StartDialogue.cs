using System.Collections;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    bool visited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
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

            if (!visited)
            {
                dialogue.PlayDialog();
            }
        }
    }
}

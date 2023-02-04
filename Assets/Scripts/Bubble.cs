using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static Vector3 Checkpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            Checkpoint = player.transform.position;
            player.GetComponentInChildren<SelfHarmer>().enabled = false;
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

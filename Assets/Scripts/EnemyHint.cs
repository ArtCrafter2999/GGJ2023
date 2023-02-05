using UnityEngine;

public class EnemyHint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            FindObjectOfType<UIController>().growInfo.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            FindObjectOfType<UIController>().growInfo.SetActive(false);
        }
    }
}

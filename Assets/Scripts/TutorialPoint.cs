using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    [TextArea] public string text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            FindObjectOfType<UIController>().StartDialog(text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            FindObjectOfType<UIController>().StopDialog();
        }
    }
}

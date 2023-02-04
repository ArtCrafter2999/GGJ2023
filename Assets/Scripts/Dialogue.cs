using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public AudioSource source;
    [TextArea] public string dialogText;

    bool isPlaying;

    private void Update()
    {
        if (isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StopDialog();
        }
    }

    public void PlayDialog()
    {
        if (isPlaying) return;

        FindObjectOfType<UIController>().StartDialog(dialogText);
        source.time = 0;
        source.Play();
        isPlaying = true;

        GameManager.Instance.PlayerComponent.enabled = false;
        GameManager.Instance.PlayerController.enabled = false;
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameManager.Instance.PlayerController.MoveInput = 0;
    }

    void StopDialog()
    {
        FindObjectOfType<UIController>().StopDialog();
        source.Stop();
        isPlaying = false;
        GameManager.Instance.PlayerComponent.enabled = true;
        GameManager.Instance.PlayerController.enabled = true;
    }
}

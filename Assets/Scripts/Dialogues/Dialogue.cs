using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public AudioSource source;
    public Phrase[] phrases;

    bool isPlaying;

    int currentPhraseIdx;

    private void Update()
    {
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || phrases[currentPhraseIdx].IsEnded)
            {
                phrases[currentPhraseIdx].Stop();

                currentPhraseIdx++;
                if (currentPhraseIdx < phrases.Length)
                {
                    phrases[currentPhraseIdx].Play(source);
                }
                else
                {
                    StopDialog();
                }
            }
        }
    }

    public void PlayDialog()
    {
        if (isPlaying) return;

        isPlaying = true;

        phrases[currentPhraseIdx].Play(source);

        GameManager.Instance.PlayerComponent.enabled = false;
        GameManager.Instance.PlayerController.enabled = false;
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameManager.Instance.PlayerController.MoveInput = 0;
    }

    void StopDialog()
    {
        FindObjectOfType<UIController>().StopDialog();
        isPlaying = false;
        GameManager.Instance.PlayerComponent.enabled = true;
        GameManager.Instance.PlayerController.enabled = true;
    }
}

using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public AudioSource source;
    public DialogData dialogData;

    bool isPlaying;
    int currentPhraseIdx;
    Phrase phrase;

    PhraseData CurrentPhraseData => dialogData.phrases[currentPhraseIdx];

    private void Awake()
    {
        phrase = new Phrase(source);
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || phrase.IsEnded)
            {
                phrase.Stop();

                currentPhraseIdx++;
                if (currentPhraseIdx < dialogData.phrases.Length)
                {
                    phrase.Play(CurrentPhraseData);
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

        phrase.Play(CurrentPhraseData);

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

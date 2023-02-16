using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public AudioSource source;
    public DialogData dialogData;

    bool isPlaying;
    int currentPhraseIdx = 0;
    Phrase phrase;

    PhraseData CurrentPhraseData => dialogData.phrases[currentPhraseIdx];

    private void Awake()
    {
        phrase = new Phrase(source);
    }
    private void Start()
    {
        GameManager.Instance.Controlls.UI.SkipDialog.performed += c => Skip();
        GameManager.Instance.Controlls.UI.Pause.performed += c => Pause();
    }

    private void Update()
    {
        if (isPlaying && phrase.IsEnded) Skip();
    }

    private void Pause()
    {
        if (phrase.IsPaused) phrase.Resume();
        else phrase.Pause();
    }    

    private void Skip()
    {
        if(isPlaying)
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

    public void PlayDialog()
    {
        if (!isPlaying)
        {
            isPlaying = true;

            phrase.Play(CurrentPhraseData);

            GameManager.Instance.PlayerComponent.enabled = false;
            GameManager.Instance.PlayerController.enabled = false;
            GameManager.Instance.Controlls.Player.Disable();
            GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            GameManager.Instance.PlayerController.MoveInput = 0;
        }
    }

    void StopDialog()
    {
        FindObjectOfType<UIController>().StopDialog();
        isPlaying = false;
        GameManager.Instance.PlayerComponent.enabled = true;
        GameManager.Instance.PlayerController.enabled = true;
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GameManager.Instance.Controlls.Player.Enable();
    }
}

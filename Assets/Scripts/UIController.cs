using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //[SerializeField]
    //private TextMeshPro _TimeText;
    [SerializeField]
    private GameObject PauseObject;

    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public GameObject harmInfo;

    void Start()
    {
        GameManager.Instance.Controlls.UI.Pause.performed += (ctx) => Pause();
    }
    private void Pause()
    {
        PauseObject.SetActive(GameManager.Instance.IsPause);
    }

    public void StartDialog(string text)
    {
        dialogPanel.SetActive(true);
        dialogText.text = text;
    }

    public void StopDialog()
    {
        dialogPanel.SetActive(false);
        dialogText.text = string.Empty;
    }

    public void ShowHarmInfo()
    {
        StartCoroutine(WaitForHideHarmInfo());
    }

    IEnumerator WaitForHideHarmInfo()
    {
        harmInfo.SetActive(true);
        yield return new WaitForSeconds(3);
        harmInfo.SetActive(false);
    }
}

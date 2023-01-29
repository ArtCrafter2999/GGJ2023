using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    //[SerializeField]
    //private TextMeshPro _TimeText;
    [SerializeField]
    private GameObject PauseObject;

    void Start()
    {
        GameManager.Instance.Controlls.UI.Pause.performed += (ctx) => Pause();
    }
    private void Pause()
    {
        PauseObject.SetActive(GameManager.Instance.IsPause);
    }
}

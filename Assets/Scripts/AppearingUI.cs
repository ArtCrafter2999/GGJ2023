using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingUI : MonoBehaviour
{
    public GameObject UI;
    public EventNode ActivateNode;
    public EventNode DeactivateNode;
    public TriggerZone Zone;

    public bool HasEnter;
    public bool HasExit;

    private void Start()
    {
        if (ActivateNode != null) ActivateNode.Event.AddListener(() => SetActive(true));
        if (DeactivateNode != null) DeactivateNode.Event.AddListener(() => SetActive(false));
        if (Zone!=null)
        {
            if (HasEnter) Zone.OnEnter.AddListener(() => SetActive(true));
            if (HasExit) Zone.OnExit.AddListener(() => SetActive(false));
        }
    }
    public void SetActive(bool active)
    {
        UI.SetActive(active);
    }
}

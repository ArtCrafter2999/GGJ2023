using System.Collections;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        dialogue.PlayDialog();
    }
}

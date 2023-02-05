using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip clip;

    AudioSource source;

    private void Awake()
    {
        source = GetComponentInParent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}

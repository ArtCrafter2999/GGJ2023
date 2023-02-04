using System.Collections;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    Vector3 initPosition;

    Vector3 spriteOffset;

    void Start()
    {
        initPosition = transform.position;
        GetComponent<Health>().OnDeath += OnDead;
        var c = GetComponent<PlayerAnimationsController>();
        spriteOffset = c.Sprite.transform.localPosition;
    }

    private void OnDead()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        
        transform.position = Bubble.Checkpoint != Vector3.zero? Bubble.Checkpoint : initPosition;

        var c = GetComponent<PlayerAnimationsController>();
        c.Sprite.gameObject.transform.parent = transform;
        c.Sprite.transform.localPosition = spriteOffset;
        GetComponent<Health>().ChangeHealth(GetComponent<Health>().MaxHealth);
        GetComponent<PlayerController>().OnEnable();
        GetComponent<Player>().enabled = true;

        c.Animator.enabled = false;
        c.Animator.enabled = true;
        c.Animator.Rebind();
        c.Animator.Update(0);
    }
}

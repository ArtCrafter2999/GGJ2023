using System.Collections;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    Vector3 spriteOffset;

    void Start()
    {
        Bubble.Checkpoint = GameManager.Instance.Player.transform.position;
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

        transform.position = Bubble.Checkpoint;

        var c = GetComponent<PlayerAnimationsController>();
        c.Sprite.gameObject.transform.parent = transform;
        c.Sprite.transform.localPosition = spriteOffset;
        GetComponent<Health>().ChangeHealth(GetComponent<Health>().MaxHealth);
        GetComponent<Player>().enabled = true;

        c.Animator.enabled = false;
        c.Animator.enabled = true;
        c.Animator.Rebind();
        c.Animator.Update(0);
    }
}

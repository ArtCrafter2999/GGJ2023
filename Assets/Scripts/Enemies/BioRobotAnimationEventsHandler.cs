using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioRobotAnimationEventsHandler : MonoBehaviour
{
    public Enemy bioRobot;
    private GameObject _player => GameManager.Instance.Player;
    public void SpawnProjectile()
    {
        Instantiate(
            bioRobot.ProjectilePrefab,
            bioRobot.Facing == Direction2.Right? bioRobot.ShootPointRight.position : bioRobot.ShootPointLeft.position,
            Quaternion.identity,
            bioRobot.transform)
            .GetComponent<Projectile>().Init(bioRobot.Facing);
    }
    public void SetPlayerInvisible()
    {
        GameManager.Instance.Player.GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.Player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().isKinematic = true;
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }
    public void SetPlayerVisible()
    {
        GameManager.Instance.Player.GetComponent<Collider2D>().enabled = true;
        GameManager.Instance.Player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().isKinematic = false;
        GameManager.Instance.Player.GetComponentInChildren<Growther>().IsGrowthing = false;
        bioRobot.FinishDeath();
        GameManager.Instance.PlayerController.health.ChangeHealth(999);
    }
}


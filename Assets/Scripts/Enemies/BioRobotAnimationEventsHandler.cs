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
        _player.SetActive(false);

    }
    public void SetPlayerVisible()
    {
        _player.SetActive(true);
        bioRobot.FinishDeath();
        GameManager.Instance.PlayerController.health.ChangeHealth(999);
    }
}


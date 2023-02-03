using UnityEngine;

public class BiorobotEnemy : Enemy
{
    public float shootRangee; 
    public Transform shootPoint;
    public Bullet bulletPrefab;
    public float bulletSpeed;
    public float heightToDetect;

    bool canShoot;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        bool onRightSide = transform.position.x < player.transform.position.x;
        bool dir = !(_right ^ onRightSide);
        bool dist = (transform.position - player.transform.position).magnitude <= shootRangee;
        bool height = Mathf.Abs(transform.position.y - player.transform.position.y) <= 2f;

        canShoot = dir && dist && height;

        isPatroling = !canShoot;
        if (canShoot)
        {
            Attack();
        }
    }

    protected override void OnAttack()
    {
        var b = Instantiate(bulletPrefab, shootPoint);
        b.Launch(bulletSpeed * (_right ? +1 : -1));
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.25f, shootRangee);
        
        Gizmos.DrawLine(transform.position + Vector3.up * heightToDetect / 2 + Vector3.right * 0.25f, transform.position + Vector3.down * heightToDetect / 2 + Vector3.right * 0.25f);
    }
}

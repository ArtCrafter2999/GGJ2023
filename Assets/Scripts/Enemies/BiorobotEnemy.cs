using UnityEngine;

public class BiorobotEnemy : Enemy
{
    public float shootRangee; 
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float bulletForce;

    bool canShoot;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        bool dir = (_right && player.LookingDirection == PlayerController.Direction2.Left ||
                    !_right && player.LookingDirection == PlayerController.Direction2.Right);
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
        b.GetComponent<Rigidbody2D>().AddForce(bulletForce * (_right ? Vector2.right : Vector2.left));
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.25f, shootRangee);
    }
}

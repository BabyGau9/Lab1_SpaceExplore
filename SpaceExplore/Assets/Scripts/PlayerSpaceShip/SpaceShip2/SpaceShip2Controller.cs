using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SpaceShip2Controller : BaseSpaceShip
{
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public Transform bulletPositionUp;
    public Transform bulletPositionLeft;
    public Transform bulletPositionRight;

    public Transform bulletPosition01;
    public Transform bulletPosition02;

    public float targetSearchRadius = 10f;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    public float homingCooldown = 5f;
    private float lastHomingTime;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireManualBullets();
        }

        if (Input.GetKeyDown(KeyCode.B) && Time.time >= lastHomingTime + homingCooldown)
        {
            FireHomingBullets();
            lastHomingTime = Time.time;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void FireManualBullets()
    {
        if (fireSound != null)
        {
            audioSource.Play();
        }

        ShootBullet(bulletPosition01, Vector2.up, bulletPrefab1);
        ShootBullet(bulletPosition02, Vector2.up, bulletPrefab1);
    }

    void FireHomingBullets()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, targetSearchRadius)
        .Where(c => c.CompareTag("EnemyShipTag") || c.CompareTag("DebrisTag"))
        .OrderBy(c => Vector2.Distance(transform.position, c.transform.position))
        .Take(3)
        .ToArray();

        if (targets.Length == 0) return;

        foreach (var target in targets)
        {
            ShootHomingBullet(target.transform);
        }
    }

    void ShootHomingBullet(Transform target)
    {
        if (target == null || bulletPrefab2 == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab2, transform.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void ShootBullet(Transform bulletPosition, Vector2 direction, GameObject bulletPrefab)
    {
        if (bulletPosition == null || bulletPrefab == null) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); 
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}

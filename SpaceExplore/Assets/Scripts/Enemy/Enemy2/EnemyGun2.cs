using UnityEngine;

public class EnemyGun2 : MonoBehaviour
{
    public GameObject EnemyBulletGo1;
    public GameObject EnemyBulletGo2;
    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.FindWithTag("PlayerShipTag");

        if (playerShip != null)
        {
            GameObject selectedBullet = (Random.value > 0.5f) ? EnemyBulletGo1 : EnemyBulletGo2;

            if (selectedBullet == null)
            {
                Debug.LogError("Selected bullet prefab is null! Check EnemyBulletGo1 and EnemyBulletGo2.");
                return;
            }

            GameObject bullet = Instantiate(selectedBullet, transform.position, Quaternion.identity);

            if (bullet == null)
            {
                Debug.LogError("Failed to instantiate bullet!");
                return;
            }

            Vector2 direction = (Random.value > 0.5f)
                ? (playerShip.transform.position - bullet.transform.position).normalized
                : Vector2.down;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            EnemyBullet2 bulletScript = bullet.GetComponent<EnemyBullet2>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
            else
            {
                Debug.LogError("Bullet prefab does not have EnemyBullet2 script attached!");
            }
        }

        Invoke("FireEnemyBullet", Random.Range(0.5f, 1.5f));
    }
}

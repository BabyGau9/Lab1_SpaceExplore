using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject EnemyBulletGo1;
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
        //get a reference to the player's ship
        GameObject playerShip = GameObject.FindWithTag("PlayerShipTag");

        if (playerShip != null) //if the player is not dead
        {
            GameObject selectedBullet = EnemyBulletGo1; 
            GameObject bullet = Instantiate(selectedBullet, transform.position, Quaternion.identity);
            Vector2 direction;
            if (Random.value > 0.5f)
            {
                direction = (playerShip.transform.position - bullet.transform.position).normalized;
            }
            else
            {
               
                direction = Vector2.down;
            }

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
        Invoke("FireEnemyBullet", Random.Range(0.5f, 1.5f));
    }
}

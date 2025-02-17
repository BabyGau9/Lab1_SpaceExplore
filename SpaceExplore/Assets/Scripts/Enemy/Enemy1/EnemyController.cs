using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject ExplosionGo;
    float speed;
    void Start()
    {
        speed = 0.5f;
    }

    void Update()
    {
        
        Vector2 position = transform.position; 

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;
    
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));   

        if (transform.position.y < min.y)   
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (ExplosionGo);

        explosion.transform.position = transform.position;
    }
}

using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject ExplosionGo;
    private int collisionCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Destroy collision of the player ship with an enemy ship, or with an enemy bullet
        if (col.tag == "PlayerShipTag")
        {
            PlayExplosion();
            Destroy(gameObject);
        }
        else if (col.tag == "PlayerBulletTag")
        {
            collisionCount++;
            Destroy(col.gameObject);
            if(collisionCount >= 4)
            {
                PlayExplosion();
                Destroy(gameObject);
            }
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGo);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}

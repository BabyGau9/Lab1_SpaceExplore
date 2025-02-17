using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float speed;
    Vector2 _directior; // the direction of the bullet
    bool isReady; // to know when the bullet direction is set

    //set default values in Awake function
    private void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    //Fuction to set the bullet's direction
    public void SetDirection(Vector2 direction)
    {
        _directior = direction.normalized;

        isReady=true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            //get the bullet's current position
            Vector2 position = transform.position;
            //compute the bullet's new position
            position += _directior * speed * Time.deltaTime;

            //update the bullet's position
            transform.position = position;

            //Next we need to remove the bullet from our game
            //if the bullet goes outside the screen

            //this is the bottom-left point of the screen 
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            // this is the top-right point of the screen
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            //if bullet goes outside the screen, then destroy it
            if((transform.position.x < min.x) || (transform.position.x > max.x) || 
               (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Destroy collision of the player ship with an enemy ship, or with an enemy bullet
        if (col.tag == "PlayerShipTag")
        {
            Destroy(gameObject);
        }
    }
}

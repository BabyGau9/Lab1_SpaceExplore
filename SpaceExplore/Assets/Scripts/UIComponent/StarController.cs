using UnityEngine;

public class StarController : MonoBehaviour
{
    float speed = 2f;

    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShipTag"))
        {
            // Award points through GameManager
            if (GameManager.instance != null)
            {
                Debug.Log("Add point.");
                GameManager.instance.AddScore(5); // Add 5 points to the score
            }
            Destroy(gameObject);
        }
        
    }
}
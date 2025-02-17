using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 8f;
    private Vector2 direction = Vector2.up;

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyShipTag") || col.CompareTag("DebrisTag"))
        {
            GameManager.instance.AddScore(3);
            Destroy(gameObject);
        }
    }
}

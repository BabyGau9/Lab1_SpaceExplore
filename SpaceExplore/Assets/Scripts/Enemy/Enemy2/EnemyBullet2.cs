using UnityEngine;

public class EnemyBullet2 : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 _directior;
    private bool isReady = false;


    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet missing Rigidbody2D!");
        }
    }


    public void SetDirection(Vector2 direction)
    {
        _directior = direction.normalized;
        isReady = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.MovePosition(rb.position + _directior * 5f * Time.fixedDeltaTime);
        }
    }
    void Update()
    {
        if (!isReady) return;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        transform.position += (Vector3)_directior * speed * Time.deltaTime;
        if (transform.position.x < min.x || transform.position.x > max.x ||
            transform.position.y < min.y || transform.position.y > max.y)
        {
            Debug.Log("Destroying bullet at position: " + transform.position);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerShipTag"))
        {
            Destroy(gameObject);
        }
    }
}

using TMPro;
using UnityEngine;

public class BaseSpaceShip : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject GameManagerGo;
    public TextMeshProUGUI LivesUIText;

    public AudioClip fireSound;
    protected AudioSource audioSource;

    protected const int MaxLives = 3;
    protected int lives;
    public float speed = 5f;

    protected virtual void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = fireSound;
        audioSource.playOnAwake = false;
        Init();
    }


    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        transform.position = Vector2.zero;
        gameObject.SetActive(true);
    }

    protected void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x -= 0.225f;
        min.x += 0.225f;
        max.y -= 0.285f;
        min.y += 0.285f;

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyShipTag") || collision.CompareTag("EnemyBulletTag") || collision.CompareTag("DebrisTag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            GameManager.instance.MinueScore(5);
            if (lives <= 0)
            {
                GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }
    protected void PlayExplosion()
    {
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class SpaceShip1Controller : BaseSpaceShip
{
    public GameObject PlayerBulletGo;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullets();
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void FireBullets()
    {
        if (fireSound != null)
        {
            audioSource.Play();
        }

        Instantiate(PlayerBulletGo, bulletPosition01.transform.position, Quaternion.identity);
        Instantiate(PlayerBulletGo, bulletPosition02.transform.position, Quaternion.identity);
    }
}

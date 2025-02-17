using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    // Xác định hướng di chuyển của màn hình hoặc camera
    ScrollDirection direction;
    // Tốc độ tạo hiệu ứng parallax
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.3f;
    Vector3 speed;
    float scrollValue;
    float lastScrollValue;

    public enum BehaviourOnExit { Destroy, Regenerate };
    // Xử lý khi đối tượng ra khỏi màn hình
    public BehaviourOnExit behaviourOnExit = BehaviourOnExit.Regenerate;

    Transform cameraTransform;
    // Giới hạn vị trí ngoài màn hình để xác định khi nào đối tượng ra ngoài màn hình
    public float limitOffScreen = 1f;

    void Start()
    {
        if (SpaceManager.instance != null)
            direction = SpaceManager.instance.scrollDirection;
        cameraTransform = Camera.main.transform;
        if (minSpeed > maxSpeed) Debug.LogError("minSpeed không thể lớn hơn maxSpeed");

        // Xác định tốc độ di chuyển tùy theo hướng di chuyển của camera
        switch (direction)
        {
            case ScrollDirection.LeftToRight:
                lastScrollValue = cameraTransform.position.x;
                speed = new Vector3(Random.Range(minSpeed, maxSpeed), 0f, 0f);
                break;
            case ScrollDirection.RightToLeft:
                lastScrollValue = cameraTransform.position.x;
                speed = new Vector3(-Random.Range(minSpeed, maxSpeed), 0f, 0f);
                break;
            case ScrollDirection.DownToUp:
                lastScrollValue = cameraTransform.position.y;
                speed = new Vector3(0f, -Random.Range(minSpeed, maxSpeed), 0f);
                break;
            case ScrollDirection.UpToDown:
                lastScrollValue = cameraTransform.position.y;
                speed = new Vector3(0f, Random.Range(minSpeed, maxSpeed), 0f);
                break;
        }
    }

    void Regenerate()
    {
        // Xử lý khi đối tượng ra khỏi màn hình và cần tái tạo lại
        switch (direction)
        {
            case ScrollDirection.LeftToRight:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1f + limitOffScreen, Random.Range(0f, 1f), 10f));
                break;
            case ScrollDirection.RightToLeft:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(-limitOffScreen, Random.Range(0f, 1f), 10f));
                break;
            case ScrollDirection.DownToUp:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1f + limitOffScreen, 10f));
                break;
            case ScrollDirection.UpToDown:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), -limitOffScreen, 10f));
                break;
        }

        // Randomize các thành phần của đối tượng con
        RandomSize[] randomSizes = gameObject.GetComponentsInChildren<RandomSize>();
        RandomRotation[] randomRotations = gameObject.GetComponentsInChildren<RandomRotation>();
        RandomColor[] randomColors = gameObject.GetComponentsInChildren<RandomColor>();
        RandomSprite[] randomSprites = gameObject.GetComponentsInChildren<RandomSprite>();

        // Randomize các thành phần trong đối tượng
        if (randomSizes != null) foreach (RandomSize r in randomSizes) r.Generate();
        if (randomRotations != null) foreach (RandomRotation r in randomRotations) r.Generate();
        if (randomColors != null) foreach (RandomColor r in randomColors) r.Generate();
        if (randomSprites != null) foreach (RandomSprite r in randomSprites) r.Generate();
    }

    void Update()
    {
        // Cập nhật vị trí di chuyển tùy theo vị trí camera
        switch (direction)
        {
            case ScrollDirection.LeftToRight:
                scrollValue = cameraTransform.position.x - lastScrollValue;
                lastScrollValue = cameraTransform.position.x;
                break;
            case ScrollDirection.RightToLeft:
                scrollValue = -cameraTransform.position.x + lastScrollValue;
                lastScrollValue = cameraTransform.position.x;
                break;
            case ScrollDirection.DownToUp:
                scrollValue = -cameraTransform.position.y + lastScrollValue;
                lastScrollValue = cameraTransform.position.y;
                break;
            case ScrollDirection.UpToDown:
                scrollValue = cameraTransform.position.y - lastScrollValue;
                lastScrollValue = cameraTransform.position.y;
                break;
        }

        // Áp dụng tốc độ di chuyển
        transform.position += speed * scrollValue;

        // Kiểm tra xem đối tượng có ra ngoài màn hình không
        switch (direction)
        {
            case ScrollDirection.LeftToRight:
                if (Camera.main.WorldToViewportPoint(transform.position).x < -limitOffScreen)
                {
                    if (behaviourOnExit == BehaviourOnExit.Regenerate) Regenerate();
                    else Destroy(gameObject);
                }
                break;
            case ScrollDirection.RightToLeft:
                if (Camera.main.WorldToViewportPoint(transform.position).x > 1f + limitOffScreen)
                {
                    if (behaviourOnExit == BehaviourOnExit.Regenerate) Regenerate();
                    else Destroy(gameObject);
                }
                break;
            case ScrollDirection.DownToUp:
                if (Camera.main.WorldToViewportPoint(transform.position).y < -limitOffScreen)
                {
                    if (behaviourOnExit == BehaviourOnExit.Regenerate) Regenerate();
                    else Destroy(gameObject);
                }
                break;
            case ScrollDirection.UpToDown:
                if (Camera.main.WorldToViewportPoint(transform.position).y > 1f + limitOffScreen)
                {
                    if (behaviourOnExit == BehaviourOnExit.Regenerate) Regenerate();
                    else Destroy(gameObject);
                }
                break;
        }
    }
}
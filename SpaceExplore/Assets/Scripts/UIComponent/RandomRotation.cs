using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float rotationSpeedMax = 35f;
    float rotationSpeed;
    public bool randomize = true;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        if (randomize)
        {
            rotationSpeed = (Random.Range(0, 100) < 50 ? -1f : 1f) * Random.Range(0f, rotationSpeedMax);
        }
        else
        {
            rotationSpeed = rotationSpeedMax;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
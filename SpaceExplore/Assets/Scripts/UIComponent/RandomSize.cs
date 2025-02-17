using UnityEngine;

public class RandomSize : MonoBehaviour
{
    [Range(1.0f, 10.0f)] public float multiplierMax = 3f;
    Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        Generate();
    }

    public void Generate()
    {
        transform.localScale = initialScale * Random.Range(1f, multiplierMax); // Randomize size
    }
}
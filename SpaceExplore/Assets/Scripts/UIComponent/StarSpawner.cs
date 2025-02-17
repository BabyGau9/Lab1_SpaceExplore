using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject Star; 
    private Coroutine spawnStarsCoroutine;
    private List<GameObject> spawnedStars = new List<GameObject>();

    public void ScheduleStarSpawner()
    {
        if (spawnStarsCoroutine == null)
        {
            spawnStarsCoroutine = StartCoroutine(SpawnStarsCoroutine());
        }
    }

    public void UnscheduleStarSpawner()
    {
        if (spawnStarsCoroutine != null)
        {
            StopCoroutine(spawnStarsCoroutine);
            spawnStarsCoroutine = null;
            DestroyAllStars();
        }
    }

    public void DestroyAllStars()
    {
        foreach (var star in spawnedStars)
        {
            Destroy(star);
        }
        spawnedStars.Clear();
    }

    void SpawnStar()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject star = Instantiate(Star);
        star.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        spawnedStars.Add(star);
    }

    IEnumerator SpawnStarsCoroutine()
    {
        while (true)
        {
            SpawnStar();
            yield return new WaitForSeconds(Random.Range(20f, 40f));
        }
    }

    public void ResumeSpawning()
    {
        if (spawnStarsCoroutine == null)
        {
            spawnStarsCoroutine = StartCoroutine(SpawnStarsCoroutine());
        }
    }

    public void PauseSpawning()
    {
        if (spawnStarsCoroutine != null)
        {
            StopCoroutine(spawnStarsCoroutine);
            spawnStarsCoroutine = null;
        }
    }
}

using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;

    public float spawnDistance = 40f;

    public float spawnWidth = 4.5f;
    public float spawnHeight = 2.5f;

    public float spawnInterval = 2f;

    // repeatedly spawns new rock instances
    void Start()
    {
        InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);
    }

    // random rock position spawning
    void SpawnRock()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        float randomY = Random.Range(-spawnHeight, spawnHeight);

        Vector3 spawnPosition = new Vector3(
            randomX,
            randomY,
            spawnDistance
        );

        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
    }

}
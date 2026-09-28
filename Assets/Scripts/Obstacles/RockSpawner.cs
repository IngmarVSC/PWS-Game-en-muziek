using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    
    public float spawnDistance = 40f;
    public float spawnWidth = 4.5f;
    public float spawnHeight = 2.5f;
    public float spawnInterval = 2f;

    public int initialRockCount = 35;

    private bool isSpawning = false;

    // repeatedly spawns new rock instances
    void Start()
    {   
    
    }   

    public void StartSpawning()
    {
        if (isSpawning)
            return;

        isSpawning = true;

        SpawnInitialField();

        InvokeRepeating(
            nameof(SpawnRock),
            1f,
            spawnInterval
        );
    }

    void SpawnInitialField()
    {   
        // instantly spawn a few rocks
        for (int i = 0; i < initialRockCount; i++)
        {
            float randomX = Random.Range(-spawnWidth, spawnWidth);
            float randomY = Random.Range(-spawnHeight, spawnHeight);

            // Spread rocks throughout the entire depth
            float randomZ = Random.Range(10f, spawnDistance);

            Vector3 spawnPosition = new Vector3(
                randomX,
                randomY,
                randomZ
            );

            Instantiate(
                rockPrefab,
                spawnPosition,
                Quaternion.identity);
            }
    }

    // random rock position spawning
    void SpawnRock()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        float randomY = Random.Range(-spawnHeight, spawnHeight);

        Vector3 spawnPosition = new Vector3(
            randomX,
            randomY,
            spawnDistance);

        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
    }
}
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    
    public float spawnDistance = 80f;
    public float spawnWidth = 4.5f;
    public float spawnHeight = 2.5f;
    public float spawnInterval = 2f;

    public float spawnTimer = 0f;
    public float spawnTimerInterval = 5f;
    public float spawnIntervalDecrease = 0.01f;
    public float minSpawnInterval = 0.001f;


    public int initialRockCount = 35;
    private bool isSpawning = false;

    
    void Start()
    {   
    
    }   

    void Update()
    {
        if (isSpawning)
        {   
            DecreaseRockSpawnTimer();
        }
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
            float randomZ = Random.Range(50f, spawnDistance);

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

    void DecreaseRockSpawnTimer()
    {
    spawnTimer += Time.deltaTime;

    if (spawnTimer >= spawnTimerInterval)
    {
        spawnInterval = Mathf.Max(
            spawnInterval - spawnIntervalDecrease,
            minSpawnInterval
        );

        spawnTimer -= spawnTimerInterval;

        // Update the repeating spawn schedule
        CancelInvoke(nameof(SpawnRock));
        InvokeRepeating(
            nameof(SpawnRock),
            0f,
            spawnInterval
        );

        Debug.Log("ROCK SPAWN INTERVAL: " + spawnInterval);
        }
    }
}
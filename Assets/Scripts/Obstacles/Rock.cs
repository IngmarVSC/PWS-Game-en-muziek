using UnityEngine;

public class Rock : MonoBehaviour
{
    public float initialSpeed = 20f;
    public float timer = 0f;
    public float speedMult = 1.2f;
    public float timeInterval = 10f;
    public float maxSpeed = 60f;


    public GameObject[] asteroidModels;

    private Vector3 randomRotationAxis;
    private float randomRotationSpeed;

    void Start()
    {
        // choose 1 of 5 asteroid models
        if (asteroidModels.Length > 0)
        {
            int randomIndex = Random.Range(0, asteroidModels.Length);
            asteroidModels[randomIndex].SetActive(true);
        }

        // generate a random rotation vector
        randomRotationAxis = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        randomRotationSpeed = Random.Range(40f, 90f); // varies rock spin speed
    }

    void Update()
    {
        // rock moves towards player
        transform.position += Vector3.back * initialSpeed * Time.deltaTime;

        // rotate rock around axis
        transform.Rotate(randomRotationAxis * randomRotationSpeed * Time.deltaTime, Space.Self);

        UpdateSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy rock at invis wall
        if (other.gameObject.CompareTag("Trigger_DestroyWall"))
        {
            Destroy(gameObject);
        }
    }

    // really basic difficulty scaler for rock speed
    // TODO: increase rock spawn rate along with rock speed
    private void UpdateSpeed()
    {
        timer += Time.deltaTime;

        if (timer >= timeInterval)
        {   
            // set upper bound to rock speed
            initialSpeed = Mathf.Min(
                initialSpeed * speedMult,
                maxSpeed
            );

            timer -= timeInterval;
        }
        Debug.Log("CURRENT ROCK SPEED " + initialSpeed);
    }
}
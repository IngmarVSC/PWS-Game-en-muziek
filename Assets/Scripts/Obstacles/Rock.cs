using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 10f;
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

        randomRotationSpeed = Random.Range(40f, 80f); // varies rock spin speed
    }

    void Update()
    {
        // rock moves towards player
        transform.position += Vector3.back * speed * Time.deltaTime;

        // rotate rock around axis
        transform.Rotate(randomRotationAxis * randomRotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy rock at invis wall
        if (other.gameObject.CompareTag("Trigger_DestroyWall"))
        {
            Destroy(gameObject);
        }
    }
}

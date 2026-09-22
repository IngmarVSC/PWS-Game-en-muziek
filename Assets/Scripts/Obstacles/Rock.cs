using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 10f;

    public GameObject[] asteroidModels;

    void Start(){
        int randomIndex = Random.Range(0, asteroidModels.Length);
        asteroidModels[randomIndex].SetActive(true);
    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger_DestroyWall"))
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 10f;

    public GameObject[] asteroidModels;

    // random asteroid mesh logic
    void Start(){
        int randomIndex = Random.Range(0, asteroidModels.Length);
        asteroidModels[randomIndex].SetActive(true);
    }

    // moving rocks towards player
    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    // destroys rocks when moving over invis wall with destroywall tag
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger_DestroyWall"))
        {
            Destroy(gameObject);
        }
    }
}
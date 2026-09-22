using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 10f;

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
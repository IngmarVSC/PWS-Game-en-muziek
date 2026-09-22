using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float moveWidth = 4.5f;
    public float moveHeight = 2.5f;

    private CharacterController controller;
    private Vector3 localOffset;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
                horizontal = -1f;
            else if (Keyboard.current.dKey.isPressed)
                horizontal = 1f;

            if (Keyboard.current.sKey.isPressed)
                vertical = -1f;
            else if (Keyboard.current.wKey.isPressed)
                vertical = 1f;
        }

        Vector3 input = new Vector3(horizontal, vertical, 0f)
                      * speed
                      * Time.deltaTime;

        Vector3 candidate = localOffset + input;

        candidate.x = Mathf.Clamp(candidate.x, -moveWidth, moveWidth);
        candidate.y = Mathf.Clamp(candidate.y, -moveHeight, moveHeight);

        Vector3 delta = candidate - localOffset;
        localOffset = candidate;

        controller.Move(delta);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Rock"))
        {
            FindAnyObjectByType<GameManager>().GameOver();
        }
    }
}

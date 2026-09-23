using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float moveWidth = 4.5f;
    public float minHeight = -1f;   // how far below the start position
    public float maxHeight = 5f;    // how far above the start position

    public float acceleration = 30f;   // how fast you speed up
    public float deceleration = 12f;   // lower = longer glide after release

    private CharacterController controller;
    private Vector3 localOffset;
    private Vector2 velocity;

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

        Vector2 targetVelocity = new Vector2(horizontal, vertical) * speed;

        // Use deceleration when there's no input, acceleration when there is
        float rate = (horizontal == 0f && vertical == 0f) ? deceleration : acceleration;
        velocity = Vector2.MoveTowards(velocity, targetVelocity, rate * Time.deltaTime);

        Vector3 candidate = localOffset + (Vector3)(velocity * Time.deltaTime);

        candidate.x = Mathf.Clamp(candidate.x, -moveWidth, moveWidth);
        candidate.y = Mathf.Clamp(candidate.y, minHeight, maxHeight);

        // Stop building up speed against the edges of the box
        if (candidate.x == -moveWidth || candidate.x == moveWidth) velocity.x = 0f;
        if (candidate.y == minHeight || candidate.y == maxHeight) velocity.y = 0f;

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
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float moveWidth = 6.5f;
    public float minHeight = -1f; // how far below the start position
    public float maxHeight = 5.5f;  // how far above the start position

    public float acceleration = 25f; // how fast you speed up
    public float deceleration = 12f; // lower = longer glide after release

    // variables for dodge function
    public float dodgeDistance = 4f; // roughly how far the dash travels sideways
    public float dodgeDuration = 0.35f; // also the length of the barrel roll
    public float dodgeCooldown = 0.8f;
    public bool invulnerableWhileDodging = true;
    public ShipBankTilt tilt;   

    private CharacterController controller;
    private Vector3 localOffset;
    private Vector2 velocity;

    // dodging flag for preventing dodging mid-dodge (and possible immunity)
    // immunity is kind of cheesy so probably not
    private bool isDodging; 
    private float dodgeTimer;
    private float dodgeCooldownTimer;
    private float dodgeDir;
    private float lastHorizontal = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (tilt == null) tilt = GetComponentInChildren<ShipBankTilt>(); // auto-find if left empty
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;
        bool dodgePressed = false;

        // player movement logic
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

            dodgePressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        }

        if (horizontal != 0f) lastHorizontal = horizontal;

        // dodge start
        dodgeCooldownTimer -= Time.deltaTime;

        // ? operator is just shortened syntax for if-else. kind of cool but maybe hard to read
        if (dodgePressed && !isDodging && dodgeCooldownTimer <= 0f)
        {
            StartDodge(horizontal != 0f ? horizontal : lastHorizontal);
        }

        // regular movement
        Vector2 targetVelocity = new Vector2(horizontal, vertical) * speed;
        
        float rate = (horizontal == 0f && vertical == 0f) ? deceleration : acceleration;
        velocity = Vector2.MoveTowards(velocity, targetVelocity, rate * Time.deltaTime);

        // dodge is a boost added on top of normal movement, not a replacement.
        // it starts at 2x average speed and eases to 0, so the total extra distance = dodgeDistance.
        float dodgeVelocityX = 0f;
        if (isDodging)
        {
            dodgeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(dodgeTimer / dodgeDuration);
            dodgeVelocityX = dodgeDir * (dodgeDistance / dodgeDuration) * 2f * (1f - t);

            if (t >= 1f)
            {
                isDodging = false;
                dodgeCooldownTimer = dodgeCooldown;
            }
        }

        Vector3 candidate = localOffset + new Vector3(
            velocity.x + dodgeVelocityX,
            velocity.y,
            0f
        ) * Time.deltaTime;

        candidate.x = Mathf.Clamp(candidate.x, -moveWidth, moveWidth);
        candidate.y = Mathf.Clamp(candidate.y, minHeight, maxHeight);

        // stop building up speed against the edges of the box. 
        // was cool tech before, but also a bug
        if (candidate.x == -moveWidth || candidate.x == moveWidth) velocity.x = 0f;
        if (candidate.y == minHeight || candidate.y == maxHeight) velocity.y = 0f;

        Vector3 delta = candidate - localOffset;
        localOffset = candidate;

        controller.Move(delta);
    }

    // dodge function
    void StartDodge(float direction)
    {
        isDodging = true;
        dodgeTimer = 0f;
        dodgeDir = direction;

        if (tilt != null) tilt.StartBarrelRoll(direction, dodgeDuration);
    }

    // checks hits with rocks and goes into game-over screen
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDodging && invulnerableWhileDodging) return;

        if (hit.gameObject.CompareTag("Rock"))
        {
            FindAnyObjectByType<GameManager>().GameOver();
        }
    }
}
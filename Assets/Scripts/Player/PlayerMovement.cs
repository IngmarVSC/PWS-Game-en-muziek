using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float moveWidth = 6.5f;
    public float minHeight = -1f;   // how far below the start position
    public float maxHeight = 5.5f;    // how far above the start position

    public float acceleration = 25f;   // how fast you speed up
    public float deceleration = 12f;   // lower = longer glide after release

    [Header("Dodge")]
    public float dodgeDistance = 4f;        // roughly how far the dash travels sideways
    public float dodgeDuration = 0.35f;     // also the length of the barrel roll
    public float dodgeCooldown = 0.8f;
    public bool invulnerableWhileDodging = true;
    public ShipBankTilt tilt;               // auto-found if left empty

    private CharacterController controller;
    private Vector3 localOffset;
    private Vector2 velocity;

    private bool isDodging;
    private float dodgeTimer;
    private float dodgeCooldownTimer;
    private float dodgeDir;
    private float lastHorizontal = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (tilt == null) tilt = GetComponentInChildren<ShipBankTilt>();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;
        bool dodgePressed = false;

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

        // Dodge start
        dodgeCooldownTimer -= Time.deltaTime;
        if (dodgePressed && !isDodging && dodgeCooldownTimer <= 0f)
        {
            StartDodge(horizontal != 0f ? horizontal : lastHorizontal);
        }

        // Normal movement
        Vector2 targetVelocity = new Vector2(horizontal, vertical) * speed;
        
        float rate = (horizontal == 0f && vertical == 0f) ? deceleration : acceleration;
        velocity = Vector2.MoveTowards(velocity, targetVelocity, rate * Time.deltaTime);

        // Dodge is a boost added on top of normal movement, not a replacement.
        // It starts at 2x average speed and eases to 0, so the total extra distance = dodgeDistance.
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

        // Stop building up speed against the edges of the box
        if (candidate.x == -moveWidth || candidate.x == moveWidth) velocity.x = 0f;
        if (candidate.y == minHeight || candidate.y == maxHeight) velocity.y = 0f;

        Vector3 delta = candidate - localOffset;
        localOffset = candidate;

        controller.Move(delta);
    }

    void StartDodge(float direction)
    {
        isDodging = true;
        dodgeTimer = 0f;
        dodgeDir = direction;

        if (tilt != null) tilt.StartBarrelRoll(direction, dodgeDuration);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDodging && invulnerableWhileDodging) return;

        if (hit.gameObject.CompareTag("Rock"))
        {
            FindAnyObjectByType<GameManager>().GameOver();
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipBankTilt : MonoBehaviour
{
    public float maxBankAngle = 35f;
    public float bankSpeed = 6f;

    public float maxPitchAngle = 10f;
    public float pitchSpeed = 10f;

    private Quaternion currentTilt;   // the normal bank/pitch, kept separate from the roll
    private bool isRolling;
    private float rollTimer;
    private float rollDuration;
    private float rollDirection;

    void Start()
    {
        currentTilt = transform.localRotation;
    }

    public void StartBarrelRoll(float direction, float duration)
    {
        isRolling = true;
        rollTimer = 0f;
        rollDuration = duration;
        rollDirection = direction;
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // ship tilting logic
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horizontal = -1f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horizontal = 1f;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                vertical = -1f;
            else if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                vertical = 1f;
        }

        Quaternion targetRotation = Quaternion.Euler(
            vertical * -maxPitchAngle,
            0f,
            horizontal * -maxBankAngle
        );

        currentTilt = Quaternion.Slerp(currentTilt, targetRotation, bankSpeed * Time.deltaTime);

        // barrel roll layered on top of the tilt
        float rollAngle = 0f;
        if (isRolling)
        {
            rollTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rollTimer / rollDuration);
            rollAngle = Mathf.SmoothStep(0f, 1f, t) * 360f * -rollDirection;
            if (t >= 1f) isRolling = false;
        }

        transform.localRotation = currentTilt * Quaternion.Euler(0f, 0f, rollAngle);
    }
}
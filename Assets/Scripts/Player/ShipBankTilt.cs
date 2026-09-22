using UnityEngine;
using UnityEngine.InputSystem;

public class ShipBankTilt : MonoBehaviour
{
    public float maxBankAngle = 35f;
    public float bankSpeed = 6f;

    public float maxPitchAngle = 10f;
    public float pitchSpeed = 10f;

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

        Quaternion targetRotation = Quaternion.Euler(
            vertical * -maxPitchAngle,
            0f,
            horizontal * -maxBankAngle
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            bankSpeed * Time.deltaTime
        );
    }
}
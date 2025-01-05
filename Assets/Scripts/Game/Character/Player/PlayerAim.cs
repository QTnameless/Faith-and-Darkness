using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera mainCamera;
    public float rotationSpeed; // Adjustable rotation speed for smoother rotation

    public PlayerMovement playerMovement;

    private void Awake()
    {
        mainCamera = Camera.main;
        // Cache the PlayerMovement component

    }

    void Update()
    {
        if (playerMovement.enabled && playerMovement.currentState != playerMovement.attackState)
        {
            LookAtMouse();
        }
    }

    private void LookAtMouse()
    {
        // Check if the player is in AttackState
        if (playerMovement.currentState == playerMovement.attackState)
        {
            // Skip rotation if in AttackState
            return;
        }

        // Get the mouse position in world coordinates
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Calculate the target angle and interpolate rotation for smooth turning
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
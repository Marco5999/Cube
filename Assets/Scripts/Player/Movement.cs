using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Maximum speed
    public float acceleration = 2f; // How fast the player accelerates
    public float deceleration = 2f; // How fast the player decelerates
    public Rigidbody2D rb;
    public Camera cam;

    public float smoothSpeed = 0.125f; // Smoothness factor for camera follow
    private Vector3 cameraOffset = new Vector3(0f, 0f, -10f); // Camera offset for 2D view

    Vector2 movement;
    Vector2 mousePos;

    Vector2 currentVelocity;

    void Update()
    {
        // Movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Mouse position for aiming (if needed)
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Calculate the target velocity
        Vector2 targetVelocity = movement.normalized * moveSpeed;

        // Smoothly accelerate or decelerate based on the player's input
        if (movement.magnitude > 0.1f)
        {
            // Accelerate towards the target velocity
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate when there is no input
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        // Apply the velocity to the rigidbody
        rb.velocity = currentVelocity;

        // Rotation
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // Smooth camera movement
        Vector3 desiredPosition = new Vector3(rb.position.x, rb.position.y, cameraOffset.z);
        Vector3 smoothedPosition = Vector3.Lerp(cam.transform.position, desiredPosition, smoothSpeed);
        cam.transform.position = smoothedPosition;

        // Ensure the camera does not rotate
        cam.transform.rotation = Quaternion.identity;
    }
}

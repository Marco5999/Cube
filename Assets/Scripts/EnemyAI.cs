using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    private Transform player;  // Reference to the player's transform
    private bool isPlayerDetected = false;  // Flag to track if the player is in detection range

    void Start()
    {
        // Find the player in the scene (assuming the player has the "Player" tag)
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isPlayerDetected && player != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Call this method to set the detection status of the player
    public void SetPlayerDetected(bool detected)
    {
        isPlayerDetected = detected;
    }

    // This method will handle the detection area events
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered detection range
            SetPlayerDetected(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited detection range
            SetPlayerDetected(false);
        }
    }
}

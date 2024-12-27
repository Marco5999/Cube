using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public int pointsForKill = 1;  // Points for this enemy
    private Transform player;  // Reference to the player's transform
    private bool isPlayerDetected = false;  // Flag to track if the player is in detection range
    public PointTracker pointTracker; // Reference to the PointTracker (no need for Inspector now)

    void Start()
    {
        // Find the player in the scene (assuming the player has the "Player" tag)
        player = GameObject.FindWithTag("Player").transform;
        
        // Find PointTracker in the scene (assuming it's attached to a UI object)
        pointTracker = FindObjectOfType<PointTracker>();
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

    // Called when the player enters the detection range
    public void SetPlayerDetected(bool detected)
    {
        isPlayerDetected = detected;
    }

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

    // Call this method when the enemy is killed
    public void KillEnemy()
    {
        // If PointTracker is found, update the points
        if (pointTracker != null)
        {
            pointTracker.UpdatePointFill(pointsForKill);  // Update the UI fill based on the points awarded
        }

        // Destroy the enemy object after being killed
        Destroy(gameObject);
    }
}

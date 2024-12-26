using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float moveSpeed = 3f;  // Speed at which the enemy moves
    public float rotationSpeed = 5f;  // Speed at which the enemy rotates towards the player

    void Update()
    {
        // Move towards the player
        MoveTowardsPlayer();

        // Rotate to face the player
        RotateTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;

        // Normalize the direction to ensure the enemy moves at a constant speed
        direction.Normalize();

        // Move the enemy towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;

        // Calculate the rotation to face the player
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Rotate smoothly towards the player
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

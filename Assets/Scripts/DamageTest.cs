using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
     public float damageAmount = 10f; // Amount of damage the player takes upon collision

    // Detect collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerHp script attached to the player object
            PlayerHp playerHp = collision.gameObject.GetComponent<PlayerHp>();

            // Apply damage to the player
            if (playerHp != null)
            {
                playerHp.TakeDamage(damageAmount);
            }
        }
    }
}

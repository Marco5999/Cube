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

    public float scaleUpDuration = 0.5f;  // Duration to scale up the enemy before destruction
    public float scaleUpFactor = 2f;  // How much to scale the enemy (double size)
    public float fadeDuration = 1f;  // Duration of the fade effect

    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;  // Reference to the sprite renderer for fade effect

    void Start()
    {
        // Find the player in the scene (assuming the player has the "Player" tag)
        player = GameObject.FindWithTag("Player").transform;
        
        // Find PointTracker in the scene (assuming it's attached to a UI object)
        pointTracker = FindObjectOfType<PointTracker>();

        // Store the original scale and sprite renderer
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        // Start the scaling and fading effect and then destroy the enemy
        StartCoroutine(ScaleAndFadeAway());
    }

    private IEnumerator ScaleAndFadeAway()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale * scaleUpFactor;  // Target size is double the original size
        Color originalColor = spriteRenderer.color;

        // While scaling up and fading out at the same time
        while (elapsedTime < Mathf.Max(scaleUpDuration, fadeDuration))
        {
            // Scaling logic
            float scaleLerpTime = Mathf.Min(elapsedTime / scaleUpDuration, 1f);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, scaleLerpTime);

            // Fading logic
            float fadeLerpTime = Mathf.Min(elapsedTime / fadeDuration, 1f);
            float alpha = Mathf.Lerp(1f, 0f, fadeLerpTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the enemy is fully transparent and at the target scale
        transform.localScale = targetScale;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Destroy the enemy after fading
        Destroy(gameObject);
    }
}

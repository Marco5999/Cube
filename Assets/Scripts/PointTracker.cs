using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointTracker : MonoBehaviour
{
     public Image pointTrackerFillImage;  // Reference to the Image component (progress bar)
    private float currentPoints = 0f;   // Current points
    public float maxPoints = 100f;      // Max points (you can adjust this to set the max points)
    public TextMeshProUGUI levelText;   // Reference to the TextMeshProUGUI component (for displaying level)
    private int currentLevel = 1;  // Current level (starting at Level 1)

    private void Start()
    {
        if (pointTrackerFillImage == null)
        {
            Debug.LogError("Point Tracker Fill Image is not assigned!");
        }

        if (levelText == null)
        {
            Debug.LogError("Level Text is not assigned!");
        }
    }

    public void UpdatePointFill(int pointsToAdd)
    {
        currentPoints += pointsToAdd;  // Add points
        currentPoints = Mathf.Clamp(currentPoints, 0, maxPoints); // Ensure we don't go over the max points

        float fillAmount = currentPoints / maxPoints;
        pointTrackerFillImage.fillAmount = fillAmount;

        if (currentPoints >= maxPoints)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentPoints = 0f;  // Reset points
        pointTrackerFillImage.fillAmount = 0f;

        currentLevel++;  // Increment level
        levelText.text = "Level " + currentLevel;  // Update level text
    }
}

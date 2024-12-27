using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTracker : MonoBehaviour
{
   public Image pointTrackerFillImage;  // Reference to the Image component (progress bar)
    private float currentPoints = 0f;   // Current points
    public float maxPoints = 100f;      // Max points (you can adjust this to set the max points)

    private void Start()
    {
        // Ensure the Point Tracker Fill Image is assigned
        if (pointTrackerFillImage == null)
        {
            Debug.LogError("Point Tracker Fill Image is not assigned!");
        }
    }

    // This method will be called to update the fill amount based on points
    public void UpdatePointFill(int pointsToAdd)
    {
        currentPoints += pointsToAdd;  // Add points
        currentPoints = Mathf.Clamp(currentPoints, 0, maxPoints); // Ensure we don't go over the max points

        // Update the fill image based on the points
        float fillAmount = currentPoints / maxPoints;  // Calculate fill amount based on current points
        pointTrackerFillImage.fillAmount = fillAmount;
    }
}

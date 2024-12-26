using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Reference to TextMeshProUGUI component
    public float updateInterval = 0.5f; // Update the FPS value every half second

    private float timePassed = 0f;
    private int frameCount = 0;
    private float currentFps = 0f;

    void Update()
    {
        // Increment the frame count and track time
        timePassed += Time.deltaTime;
        frameCount++;

        // Update the FPS value after a certain interval
        if (timePassed >= updateInterval)
        {
            currentFps = frameCount / timePassed;
            timePassed = 0f;
            frameCount = 0;

            // Update the FPS text on screen
            if (fpsText != null)
            {
                fpsText.text = "FPS: " + Mathf.Floor(currentFps).ToString();
            }
        }
    }
}

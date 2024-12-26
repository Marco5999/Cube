using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera camera; // Reference to the camera
    public float zoomSpeed = 10f; // Speed of zoom
    public float minSize = 5f; // Minimum orthographic size
    public float maxSize = 20f; // Maximum orthographic size

    void Update()
    {
        // Get the scroll wheel input (positive for zooming in, negative for zooming out)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Modify the camera's orthographic size based on the scroll wheel input
        camera.orthographicSize -= scrollInput * zoomSpeed;

        // Clamp the orthographic size value to stay within the min and max limits
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minSize, maxSize);
    }
}

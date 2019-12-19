using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform coin; // Transform of coin to follow

    bool dragging; // Whether or not mouse button is held or finger is pressed on screen
    bool wasPinching = false; // Was the user pinching the last time we checked
    float pinchDistance = 0; // Distance between 2 fingers while pinching

    // Clamp values for pitch rotation
    float maxPitch = 90;
    float minPitch = 0;

    
    void Start()
    {
        // Grab the coin's transform
        coin = FindObjectOfType<CoinController>().transform;
    }

    
    void Update()
    {
        // Follow coin
        transform.position = coin.position;

        // Check if mouse has just been pressed or finger has just touched
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot a raycast from touch position
            RaycastHit hit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mouseRay, out hit);

            // Make sure we are not touching the coin
            if (hit.collider == null || hit.collider.GetComponent<CoinController>() == null)
            {
                dragging = true;
            }
        }

        // Stop dragging of the touch / mouse press is released
        if (Input.GetMouseButtonUp(0)) dragging = false;

        // Handle zoom and rotation
        rotate();
        zoom();
    }

    /**
     * Handle zoom
     */
    private void zoom()
    {
        // Only zoom if there are 2 fingers touching screen
        if (Input.touchCount == 2)
        {
            // Get the positions of each touch
            Vector2 a = Input.touches[0].position;
            Vector2 b = Input.touches[1].position;

            // Get the distance between the touches
            float newDistance = (a - b).magnitude;

            // Zoom if this is not the first frame of the fingers touching (we have an old distance)
            if (wasPinching)
            {
                Camera.main.fieldOfView += (pinchDistance - newDistance) / 10;
            }

            // Remember this distance
            pinchDistance = newDistance;
            wasPinching = true;
        }
        else wasPinching = false;
    }
    
    /**
     * Handle rotation
     */
    private void rotate()
    {
        // Make sure there is either 1 finger or 0 (mouse is held) and we are dragging
        if (Input.touchCount < 2 && dragging)
        {
            float x;
            float y;
            // Grab position of touch
            if (Input.touchCount == 1)
            {
                x = Input.touches[0].deltaPosition.x;
                y = Input.touches[0].deltaPosition.y;
            }
            // Grab position of mouse press
            else
            {
                x = Input.GetAxisRaw("Mouse X");
                y = Input.GetAxisRaw("Mouse Y");
            }
            
            // Calculate change in yaw rotation
            float yawDelta = x / 4;

            // Calculate change in pitch rotation
            float currentPitch = transform.localRotation.eulerAngles.x;
            float pitchDelta = Mathf.Min(Mathf.Max(-y / 8, minPitch - currentPitch), maxPitch - currentPitch);

            // Rotate object
            transform.Rotate(new Vector3(0, yawDelta, 0), Space.World);
            transform.Rotate(new Vector3(pitchDelta, 0, 0), Space.Self);
        }
    }

}

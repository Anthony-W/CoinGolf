using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform cam;
    Transform coin;
    bool dragging;
    bool wasPinching = false;
    float pinchDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        coin = FindObjectOfType<CoinController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow coin
        transform.position = coin.position;


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(mouseRay, out hit);
            if (hit.collider == null || hit.collider.GetComponent<CoinController>() == null)
            {
                dragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) dragging = false;

        if (Input.touchCount == 1 && dragging)
        {
            float x = Input.touches[0].deltaPosition.x;
            float y = Input.touches[0].deltaPosition.y;

            //transform.Rotate(Vector3.up, x);
            transform.Rotate(new Vector3(0, x/4, 0), Space.World);
            transform.Rotate(new Vector3(-y/8, 0, 0), Space.Self);
            //cam.LookAt(transform.position+Vector3.down*drop);
        }

        if (Input.touchCount == 2)
        {
            Vector2 a = Input.touches[0].position;
            Vector2 b = Input.touches[1].position;
            float newDistance = (a - b).magnitude;
            if (wasPinching)
            {
                Camera.main.fieldOfView += (pinchDistance - newDistance) / 10;
            }
            pinchDistance = newDistance;
            wasPinching = true;
        }
        else wasPinching = false;
    }
}

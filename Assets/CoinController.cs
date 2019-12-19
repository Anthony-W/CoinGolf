using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] float maxDraw = 2.5f; // How far back you can pull
    [SerializeField] float forceMultiplier = 150; // Increase the force applied

    Rigidbody rb;
    Camera cam;
    ExtendableArrow arrow;

    bool drawing = false;
    bool moving = false;
    Vector3 force = new Vector3(0, 0, 0);
    Vector3 mousePos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow = GetComponentInChildren<ExtendableArrow>(true);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (rb.velocity.magnitude < 0.01) moving = false;
        }

        // Raycast from mouse to table
        if (Input.GetMouseButtonDown(0))
        {
            if (!moving && coinClicked()) drawing = true;
        }
        if (drawing)
        {
            if (Input.GetMouseButton(0)) updateForce();
            if (Input.GetMouseButtonUp(0))
            {
                drawing = false;
                applyForce();
                GameManager.Instance().AddStroke();
                moving = true;
            }
        }
        else // Not drawing
        {
            arrow.hideArrow();
        }
    }

    bool coinClicked()
    {
        RaycastHit hit;
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(mouseRay, out hit);
        if (hit.collider != null && hit.collider.GetComponent<CoinController>() == this)
        {
            return true;
        }
        return false;
    }

    void updateForce()
    {
        // get coin position
        Vector3 coinPos = transform.position;
        // remove y component
        coinPos.y = 0;

        // get mouse position
        RaycastHit[] hits;
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(mouseRay);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.GetComponent<Table>()) mousePos = hits[i].point;
        }
        mousePos.y = 0;

        // calculate force
        Vector3 diff = coinPos - mousePos;
        if (diff.magnitude > maxDraw) diff *= maxDraw / diff.magnitude;
        force = (diff) * forceMultiplier;

        // Display arrow to visualize force
        arrow.setLength(diff.magnitude/maxDraw);
        arrow.lookOpposite(mousePos);
        arrow.showArrow();
    }

    void applyForce()
    {
        rb.AddForce(force);
    }

    public bool isMoving()
    {
        return moving;
    }
}

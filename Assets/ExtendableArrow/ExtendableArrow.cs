using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendableArrow : MonoBehaviour
{
    // Gameobjects used to build arrow
    [SerializeField] GameObject arrowHead = null;
    [SerializeField] GameObject arrowBase = null;

    // Max length of arrow
    [SerializeField] float maxLength = 5;

    // Multiplier for stretch of base;
    [SerializeField] float scaleMultiplier = 2;

    // Variables to store arrow pieces
    GameObject aHead, aBase;

    /**
     * Initialize pieces of arrow and make it invisible
     */
    void Start()
    {
        hideArrow();
        aHead = Instantiate(arrowHead, transform);
        aBase = Instantiate(arrowBase, transform);
    }

    /**
     * Set the desired length of an arrow
     * param: targetLength - float between 0 and 1 represented relative size of arrow
     */
    public void setLength(float targetLength) // 0-1
    {
        // Get the actual desired length
        targetLength *= maxLength;

        // Don't go past max length
        float length = Mathf.Min(targetLength, maxLength);

        // Set positions of arrow head and base
        aHead.transform.localPosition = Vector3.forward * (length+0.2f);
        aBase.transform.localPosition = Vector3.forward * length/2;

        // Set scale for arrow base
        Vector3 scale = aBase.transform.localScale;
        scale.y = targetLength * scaleMultiplier; // scaling y because base gameobject is weird
        aBase.transform.localScale = scale;
    }

    /**
     * make arrow point directly at a target (ignores differences in y)
     */
    public void lookAt(Vector3 target)
    {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    /**
     * Similar to look at, but looks directly away from target instead
     */
    public void lookOpposite(Vector3 target)
    {
        lookAt(transform.position*2 - target);
    }

    /**
     * Make the arrow invisible
     */
    public void showArrow()
    {
        gameObject.SetActive(true);
    }

    /**
     * Make the arrow visible
     */
    public void hideArrow()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StrokeCounter;
    [SerializeField] TextMeshProUGUI Par;
    [SerializeField] GameObject StarHolder;

    Animator[] stars;

    // Start is called before the first frame update
    void Start()
    {
        Par.text = "Par: " + GameManager.Instance().GetPar().ToString();
        stars = StarHolder.GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStrokes(int strokes)
    {
        StrokeCounter.text = "Strokes: " + strokes.ToString();
    }

    public void ShowStars(int numStars)
    {
        StartCoroutine(ShowStarsCoroutine(numStars));
    }

    private IEnumerator ShowStarsCoroutine(int numStars)
    {
        for (int i = 0; i < numStars; i++)
        {
            stars[i].SetTrigger("Appear");
            yield return new WaitForSeconds(0.2f);
        }
    }
}

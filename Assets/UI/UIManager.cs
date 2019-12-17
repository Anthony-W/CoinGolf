using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StrokeCounter = null;
    [SerializeField] TextMeshProUGUI Par = null;
    [SerializeField] GameObject StarHolder = null;
    [SerializeField] GameObject EndPanel = null;

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
        showUI();
        StartCoroutine(ShowStarsCoroutine(numStars));
    }

    private IEnumerator ShowStarsCoroutine(int numStars)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < numStars; i++)
        {
            
            stars[i].SetTrigger("Appear");
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void showUI()
    {
        EndPanel.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
    }
}

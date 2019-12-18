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

    [SerializeField] GameObject WinPanel = null;
    [SerializeField] GameObject LosePanel = null;
    [SerializeField] GameObject ChallengeWinPanel = null;

    ChallengeManager cm = null;

    Animator[] stars;

    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<ChallengeManager>();
        if (cm != null) // Playing the game in challenge mode
        {
            Par.text = "Level " + cm.currentMap + "/3";
            StrokeCounter.text = "Strokes: " + cm.strokes + "/" + cm.par;
        }
        else // Player individual levels
        {
            Par.text = "Par: " + GameManager.Instance().GetPar().ToString();
            stars = StarHolder.GetComponentsInChildren<Animator>();
        }

        
    }

    public void UpdateStrokes(int strokes)
    {
        if (cm != null)
        {
            StrokeCounter.text = "Strokes: " + cm.strokes + "/" + cm.par;
        }
        else
        {
            StrokeCounter.text = "Strokes: " + strokes.ToString();
        }
    }

    public void ShowStars(int numStars)
    {
        showWinPanel();
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

    private void showWinPanel()
    {
        WinPanel.SetActive(true);
    }

    public void showLossPanel()
    {
        LosePanel.SetActive(true);
    }

    public void showChallengeWinPanel()
    {
        ChallengeWinPanel.SetActive(true);
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

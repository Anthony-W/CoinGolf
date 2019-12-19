using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Misc GUI that needs to be updated
    [SerializeField] TextMeshProUGUI StrokeCounter = null;
    [SerializeField] TextMeshProUGUI Par = null;
    [SerializeField] GameObject StarHolder = null;

    // Panels only shown once level is completed, via win or loss
    [SerializeField] GameObject WinPanel = null;
    [SerializeField] GameObject LosePanel = null;
    [SerializeField] GameObject ChallengeWinPanel = null;
    [SerializeField] GameObject fadeBackground = null;

    // All titles that need to be updated with level name
    [SerializeField] TextMeshProUGUI[] Titles = null;

    ChallengeManager cm = null;

    // Animators for starts
    Animator[] stars;

    // Start is called before the first frame update
    void Start()
    {
        // Update every title
        updateTitles();

        // Check for challenge manager
        cm = FindObjectOfType<ChallengeManager>();

        if (cm != null) // Playing the game in challenge mode
        {
            Par.text = "Level " + cm.currentMap + "/3";
            StrokeCounter.text = "Strokes: " + cm.strokes + "/" + cm.par;
        }
        else // Playing individual levels
        {
            Par.text = "Par: " + GameManager.Instance().GetPar().ToString();
            stars = StarHolder.GetComponentsInChildren<Animator>();
        }
    }

    /**
     * Update stroke counter in UI
     */
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

    /**
     * Start star animations
     */
    public void ShowStars(int numStars)
    {
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

    /**
     * Update every title with the correct level name
     */
    private void updateTitles()
    {
        string name = GameManager.Instance().GetLevelName();
        foreach (TextMeshProUGUI text in Titles)
        {
            text.text = name;
        }
    }

    #region End Panels

    /**
     * Show the win panel
     */
    public void showWinPanel()
    {
        WinPanel.SetActive(true);
        fadeBackground.SetActive(true);
    }

    /**
     * Show the loss panel
     */
    public void showLossPanel()
    {
        LosePanel.SetActive(true);
        fadeBackground.SetActive(true);
    }

    /**
     * Show the win panel for challenge mode
     */
    public void showChallengeWinPanel()
    {
        ChallengeWinPanel.SetActive(true);
        fadeBackground.SetActive(true);
    }
    #endregion

    #region Buttons

    /**
     * Load the next level, if possible
     */
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    /**
     * Replay the current level
     */
    public void ReplayLevel()
    {
        // If this is challenge mode, we must reset the entire challenge
        if (cm != null)
        {
            cm.resetChallenge();
        }
        // If this is not challenge mode, we can just reload the level
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /**
     * Go back to main menu
     */
    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}

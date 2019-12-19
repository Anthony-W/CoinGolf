using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Maximum number of strokes to achieve each level of stars
    [SerializeField] int threeStarStrokes = 1;
    [SerializeField] int twoStarStrokes = 2;
    [SerializeField] int oneStarStrokes = 3;

    // level-unique numbers
    [SerializeField] int sectionNumber = 0;
    [SerializeField] int levelNumber = 0;

    // ChallengeManager, only used if playing challenge mode
    ChallengeManager cm = null;

    CoinController coin = null;

    // Specific instance of this script
    private static GameManager gm;
    public static GameManager Instance()
    {
        if (gm == null)
            gm = FindObjectOfType<GameManager>();
        return gm;
    }

    // Current number of strokes
    int strokes = 0;

    // Whether or not coin was moving last update
    bool coinWasMoving = false;

    // UIManager for updating ui
    UIManager ui;
    
    // Start is called before the first frame update
    void Start()
    {
        // Grab UIManager instance
        ui = FindObjectOfType<UIManager>();

        // Try to grab ChallengeManager instace, won't be able to if we aren't in challenge mode
        cm = FindObjectOfType<ChallengeManager>();
        if (cm != null)
        {
            // Update current strokes from challenge strokes
            strokes = cm.strokes;
        }

        coin = FindObjectOfType<CoinController>();
    }

    private void Update()
    {

        bool coinMoving = coin.isMoving();
        if (!coinMoving && coinWasMoving)
        {
            if (strokes == oneStarStrokes) HandleLoss();
        }
        coinWasMoving = coinMoving;
    }

    /**
     * User has beaten the level
     */
    public void HandleWin()
    {
        // if in challenge mode
        if (cm != null)
        {
            if (cm.currentMap == 3) // beat the whole challenge
            {
                ui.showChallengeWinPanel();
                // Table 1 Challenge
                // 11/12 strokes used

                // Destroy the no longer necessary challenge manager
                Destroy(cm.gameObject);
            }
            else
            {
                // Load the next map of this challenge
                cm.currentMap++;
                string nextMap = cm.sectionNumber + "-" + cm.currentMap;
                SceneManager.LoadScene(nextMap);
            }
        }
        // if not in challenge mode
        else
        {
            // Calculate how many stars user recieved
            int stars = 0;
            if (strokes <= threeStarStrokes) stars = 3;
            else if (strokes <= twoStarStrokes) stars = 2;
            else if (strokes <= oneStarStrokes) stars = 1;

            // Unhide the win panel and update the stars
            ui.showWinPanel();
            ui.ShowStars(stars);
        }
    }

    /**
     * User has lost
     * Either used too many strokes or fell off
     */
    public void HandleLoss()
    {
        print("You lost!");
        ui.showLossPanel();
    }

    /**
     * User has taken another stroke, increment counter and check for loss
     */
    public void AddStroke()
    {
        if (cm != null)
        {
            cm.strokes++;
            if (cm.strokes > cm.par)
            {
                HandleLoss();
            }
        }
        else
        {
            strokes++;
        }
        
        UpdateStrokeCounter();
    }

    /**
     * Update strokes in UI
     */
    private void UpdateStrokeCounter()
    {
        ui.UpdateStrokes(strokes);
    }

    /**
     * returns par for this level
     */
    public int GetPar()
    {
        return threeStarStrokes;
    }

    /**
     * returns the name of the level
     */
    public string GetLevelName()
    {
        string name = sectionNumber + "-";
        if (levelNumber != 4) // 4 is Hole-in-One
        {
            name += levelNumber;
        }
        else
        {
            name += "Hole-in-One";
        }

        return name;
    }
}

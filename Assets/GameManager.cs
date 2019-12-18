using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int threeStarStrokes = 1;
    [SerializeField] int twoStarStrokes = 2;
    [SerializeField] int oneStarStrokes = 3;

    ChallengeManager cm = null;

    private static GameManager gm;
    public static GameManager Instance()
    {
        if (gm == null)
            gm = FindObjectOfType<GameManager>();
        return gm;
    }

    int strokes = 0;
    UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        cm = FindObjectOfType<ChallengeManager>();
        if (cm != null)
        {
            strokes = cm.strokes;
        }
    }

    public void HandleWin()
    {
        if (cm != null)
        {
            if (cm.currentMap == 3) // Actual win
            {
                ui.showChallengeWinPanel();
                // Table 1 Challenge
                // 11/12
                print("You win!");
                Destroy(cm.gameObject);
                //SceneManager.LoadScene(0);
            }
            else
            {
                cm.currentMap++;
                string nextMap = cm.sectionNumber + "-" + cm.currentMap;
                SceneManager.LoadScene(nextMap);
            }
        }
        else
        {
            int stars = 0;
            if (strokes <= threeStarStrokes) stars = 3;
            else if (strokes <= twoStarStrokes) stars = 2;
            else if (strokes <= oneStarStrokes) stars = 1;

            ui.ShowStars(stars);
            print("You used " + strokes + " strokes and earned " + stars + " stars!");
        }
    }

    public void HandleLoss()
    {
        print("You lost!");
        // TODO: UI menu to give player options
        ui.showLossPanel();
    }

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

    private void UpdateStrokeCounter()
    {
        ui.UpdateStrokes(strokes);
    }

    public int GetPar()
    {
        return threeStarStrokes;
    }
}

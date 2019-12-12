using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int threeStarStrokes = 1;
    [SerializeField] int twoStarStrokes = 2;
    [SerializeField] int oneStarStrokes = 3;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleWin()
    {
        int stars = 0;
        if (strokes <= threeStarStrokes) stars = 3;
        else if (strokes <= twoStarStrokes) stars = 2;
        else if (strokes <= oneStarStrokes) stars = 1;

        ui.ShowStars(stars);
        print("You used " + strokes + " strokes and earned " + stars + " stars!");
    }

    public void HandleLoss()
    {
        print("You lost!");
        // TODO: UI menu to give player options
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddStroke()
    {
        strokes++;
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

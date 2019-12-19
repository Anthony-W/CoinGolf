using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    public int strokes = 0; // total strokes used
    public int par = 0; // challenge par
    public int currentMap = 1; // current map in this challenge
    public int sectionNumber = 0; // challenge section

    // Start is called before the first frame update
    void Start()
    {
        // Let challenge manager exist across multiple scenes
        // We load each level separately, this simplifies carrying over challenge manager
        DontDestroyOnLoad(gameObject);
    }

    /**
     * Set par for this challenge
     */
    public void setPar(int par)
    {
        this.par = par;
    }

    /**
     * Set section number for this challenge
     */
    public void setSectionNumber(int section)
    {
        sectionNumber = section;
    }

    /**
     * Reset challenge so it can be replayed
     */
    public void resetChallenge()
    {
        strokes = 0;
        currentMap = 1;
        SceneManager.LoadScene(sectionNumber + "-1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectSection : MonoBehaviour
{
    [SerializeField] int SectionNumber = 0;
    [SerializeField] int ChallengePar = 0;

    /** 
     * Load a single level on its own
     */
    public void LoadLevel(int i)
    {
        string levelName = SectionNumber.ToString() + "-" + i.ToString();
        SceneManager.LoadScene(levelName);
    }

    /** 
     * Load the challenge for this sections
     */
    public void LoadChallenge()
    {
        string levelName = SectionNumber.ToString() + "-1";

        // Create the challenge manager
        GameObject obj = (GameObject)Instantiate(Resources.Load("ChallengeManager"));
        ChallengeManager cm = obj.GetComponent<ChallengeManager>();

        // Set up challenge data
        cm.setPar(ChallengePar);
        cm.setSectionNumber(SectionNumber);

        // Load the first level of the challenge
        SceneManager.LoadScene(levelName);
    }

    /**
     * Loads the hole-in-one map for this section
     */
    public void LoadHoleInOne()
    {
        string levelName = SectionNumber.ToString() + "-HoleInOne";
        SceneManager.LoadScene(levelName);
    }

    /**
     * Go back to section select
     */
    public void Back()
    {
        MenuManager.LoadLevelSelect();
    }
}

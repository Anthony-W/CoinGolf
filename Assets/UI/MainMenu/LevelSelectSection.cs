using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectSection : MonoBehaviour
{
    [SerializeField] int SectionNumber = 0;
    [SerializeField] int ChallengePar = 0;
    

    public void LoadLevel(int i)
    {
        string levelName = SectionNumber.ToString() + "-" + i.ToString();
        SceneManager.LoadScene(levelName);
    }

    public void LoadChallenge()
    {
        string levelName = SectionNumber.ToString() + "-1";

        GameObject obj = (GameObject)Instantiate(Resources.Load("ChallengeManager"));
        ChallengeManager cm = obj.GetComponent<ChallengeManager>();
        cm.setPar(ChallengePar);
        cm.setSectionNumber(SectionNumber);

        SceneManager.LoadScene(levelName);
    }

    public void LoadHoleInOne()
    {
        string levelName = SectionNumber.ToString() + "-HoleInOne";
        SceneManager.LoadScene(levelName);
    }

    public void Back()
    {
        MenuManager.LoadLevelSelect();
    }
}

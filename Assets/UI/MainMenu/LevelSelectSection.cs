using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectSection : MonoBehaviour
{
    [SerializeField] int SectionNumber = 0;

    public void LoadLevel(int i)
    {
        string levelName = SectionNumber.ToString() + "-" + i.ToString();
        SceneManager.LoadScene(levelName);
    }

    public void Back()
    {
        MenuManager.LoadLevelSelect();
    }
}

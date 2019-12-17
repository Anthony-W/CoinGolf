using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBase : MonoBehaviour
{
    public void LoadSection(int i)
    {
        MenuManager.LoadLevelSection(i-1);
    }

    public void Home()
    {
        MenuManager.LoadMainMenu();
    }
}

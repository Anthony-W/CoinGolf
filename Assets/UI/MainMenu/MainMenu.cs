using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect()
    {
        MenuManager.LoadLevelSelect();
    }

    public void ChallengeMode()
    {
        print("Challenge Mode panel is not ready yet");
    }

    public void HoleInOne()
    {
        print("Hole-in-One panel is not ready yet");
    }

    public void Cosmetics()
    {
        print("Cosmetics panel is not ready yet");
    }
}

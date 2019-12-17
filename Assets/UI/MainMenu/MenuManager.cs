using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] RectTransform MainMenu = null;
    [SerializeField] RectTransform LevelSelectBase = null;
    [SerializeField] RectTransform[] LevelSelectSections = null;

    static MenuManager instance;
    static RectTransform currentlyLoaded = null;

    public void Start()
    {
        instance = this;
        LoadMainMenu();
    }

    public static void LoadMainMenu()
    {
        LoadPanel(instance.MainMenu);
    }

    public static void LoadLevelSelect()
    {
        LoadPanel(instance.LevelSelectBase);
    }

    public static void LoadLevelSection(int section)
    {
        LoadPanel(instance.LevelSelectSections[section]);
    }

    #region Helper Methods

    private static void LoadPanel(RectTransform panel)
    {
        panel.position = new Vector3(540, 960);

        if (currentlyLoaded != null && currentlyLoaded != panel)
            UnloadPanel(currentlyLoaded);

        currentlyLoaded = panel;
    }

    private static void UnloadPanel(RectTransform panel)
    {
        panel.position = Vector3.left * 5000;
    }

    #endregion
}

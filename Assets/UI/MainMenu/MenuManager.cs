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

    /**
     * Load the base main menu panel
     */
    public static void LoadMainMenu()
    {
        LoadPanel(instance.MainMenu);
    }

    /**
     * Loads a panel for selecting a section
     */
    public static void LoadLevelSelect()
    {
        LoadPanel(instance.LevelSelectBase);
    }

    /**
     * Loads a panel for selecting levels from a given section
     */
    public static void LoadLevelSection(int section)
    {
        LoadPanel(instance.LevelSelectSections[section]);
    }

    #region Helper Methods

    /**
     * Remove old panel and put a new one on screen
     */
    private static void LoadPanel(RectTransform panel)
    {
        panel.position = new Vector3(540, 960);

        if (currentlyLoaded != null && currentlyLoaded != panel)
            UnloadPanel(currentlyLoaded);

        currentlyLoaded = panel;
    }

    /**
     * Remove panel from screen
     */
    private static void UnloadPanel(RectTransform panel)
    {
        panel.position = Vector3.left * 5000;
    }

    #endregion
}

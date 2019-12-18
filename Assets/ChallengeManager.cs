using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public int strokes = 0;
    public int par = 0;
    public int currentMap = 1;
    public int sectionNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setPar(int par)
    {
        this.par = par;
    }

    public void setSectionNumber(int section)
    {
        sectionNumber = section;
    }
}

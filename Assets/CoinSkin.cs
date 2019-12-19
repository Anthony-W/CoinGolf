using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSkin : MonoBehaviour
{
    [SerializeField] Material skin = null;

    // Start is called before the first frame update
    void Start()
    {
        destroyOld();

        DontDestroyOnLoad(gameObject);

    }

    public void SetSkin(Material skin)
    {
        this.skin = skin;
    }

    public Material GetSkin()
    {
        return skin;
    }

    private void destroyOld()
    {
        CoinSkin[] instances = FindObjectsOfType<CoinSkin>();
        if (instances.Length > 1)
        {
            foreach (CoinSkin i in instances)
            {
                if (i != this)
                {
                    SetSkin(i.GetSkin());
                    Destroy(i.gameObject);
                }
            }
            
        }
    }


}

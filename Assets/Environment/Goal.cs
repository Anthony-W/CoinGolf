using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Rigidbody coin = null;
    bool gameOver = false;

    private void Update()
    {
        if (coin != null)
        {
            if (!gameOver && coin.velocity.magnitude < 0.01)
            {
                GameManager.Instance().HandleWin();
                gameOver = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CoinController>() != null) coin = other.GetComponent<Rigidbody>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CoinController>() != null) coin = null;
    }
}

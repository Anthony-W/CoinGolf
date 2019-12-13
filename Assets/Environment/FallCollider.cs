using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CoinController>() != null)
        {
            GameManager.Instance().HandleLoss();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ActiveUI(GameState.GameClear);
        }
    }
}

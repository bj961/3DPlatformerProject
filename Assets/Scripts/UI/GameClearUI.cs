using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    void Start()
    {
        if (UIManager.Instance.GameClearUI == null)
        {
            UIManager.Instance.GameClearUI = gameObject;
        }
        gameObject.SetActive(false);
    }
}

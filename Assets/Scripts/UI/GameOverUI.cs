using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        if (UIManager.Instance.GameOverUI == null)
        {
            UIManager.Instance.GameOverUI = gameObject;
        }
        gameObject.SetActive(false);
    }
}

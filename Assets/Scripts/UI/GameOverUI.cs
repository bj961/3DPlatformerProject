using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        if(UIManager_tmp.Instance.tmp_GameOverUI == null)
        {
            UIManager_tmp.Instance.tmp_GameOverUI = gameObject;
        }
        gameObject.SetActive(false);
    }
}

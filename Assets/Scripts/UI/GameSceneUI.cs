using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    void Start()
    {
        if(UIManager.Instance.StartSceneUI == null)
        {
            UIManager.Instance.StartSceneUI = gameObject;
        }
        gameObject.SetActive(false);
    }
}

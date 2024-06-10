using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    void Start()
    {
        if(UIManager.Instance.InGameUI == null)
        {
            UIManager.Instance.InGameUI = gameObject;
        }
        gameObject.SetActive(false);
    }
}

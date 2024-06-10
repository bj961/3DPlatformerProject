using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    void Update()
    {
        if(UIManager.Instance.InGameUI != null)
        {
            GameManager.Instance.GameStartState();
            Destroy(gameObject);
        }
        
    }
}

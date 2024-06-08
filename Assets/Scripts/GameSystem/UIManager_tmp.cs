using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_tmp : MonoBehaviour
{
    public static UIManager_tmp Instance;



    public GameObject tmp_GameOverUIPrefab;
    public GameObject tmp_GameOverUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
        }


        if (tmp_GameOverUI == null)
        {
            Debug.Log("UI 생성");
            tmp_GameOverUI = Instantiate(tmp_GameOverUIPrefab);
            if (tmp_GameOverUI == null)
            {
                Debug.Log("UI 연결 안됨");
            }
        }

    }

    public void ActiveUI(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Intro:
                //IntroUI.setActive(true)
                break;
            case GameState.GameStart:
                //GameStartUI.setActive(true);
                break;
            case GameState.GameOver:
                tmp_GameOverUI.SetActive(true);
                break;
            case GameState.GameClear:
                //GameClearUI.setActive(true);
                break;
        }
    }
}

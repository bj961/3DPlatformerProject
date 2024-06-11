using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    // TODO : 추후 Dictionary 사용하여 관리토록 리팩토링
    public GameObject GameOverUIPrefab;
    public GameObject GameOverUI;

    public GameObject GameClearUIPrefab;
    public GameObject GameClearUI;

    public GameObject InGameUIPrefab;
    public GameObject InGameUI;


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

        if (GameOverUI == null)
        {
            GameOverUI = Instantiate(GameOverUIPrefab);
        }
        if (GameClearUI == null)
        {
            GameClearUI = Instantiate(GameClearUIPrefab);
        }
        if (InGameUI == null)
        {
            InGameUI = Instantiate(InGameUIPrefab);
        }
    }

    public void ActiveUI(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Intro:
                //IntroUI.SetActive(true)
                Debug.Log("인트로씬 UI");
                break;
            case GameState.GameStart:
                InGameUI.SetActive(true);
                break;
            case GameState.GameOver:
                GameOverUI.SetActive(true);
                break;
            case GameState.GameClear:
                GameClearUI.SetActive(true);
                break;
        }
    }
}

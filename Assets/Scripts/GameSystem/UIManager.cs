using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    // TODO : ���� �� ���� UI�� Dictionary ����Ͽ� ������� �����丵
    public GameObject GameOverUIPrefab;
    public GameObject GameOverUI;

    public GameObject GameClearUIPrefab;
    public GameObject GameClearUI;

    //�̻��
    public GameObject StartSceneUIPrefab;
    public GameObject StartSceneUI;


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
    }

    public void ActiveUI(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Intro:
                //IntroUI.SetActive(true)
                Debug.Log("��Ʈ�ξ� UI");
                break;
            case GameState.GameStart:
                //StartSceneUI.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Intro,
    GameStart,
    GameOver,
    GameClear
}

public enum GameStage
{
    Stage1,
    Stage2
}

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _Instance;
        }
    }

    public InGameController controller;

    public GameState currentGameState { get; private set; }
    public GameStage currentStage { get; private set; }
    public void SetStage(GameStage newStage)
    {
        currentStage = newStage;
    }

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;

            Application.targetFrameRate = 60;
            currentGameState = GameState.Intro;
            currentStage = GameStage.Stage1;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_Instance != null)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        if (controller == null)
        {
            controller = new GameObject("InGameController").AddComponent<InGameController>();
        }
    }

    // State : 시작화면
    public void IntroState()
    {
        currentGameState = GameState.Intro;

        // TODO : 
        // LoadScene(IntroScene);
        // SoundManager.Instance.PlayBGM("IntroBGM");
    }

    // State : 게임 시작
    public void GameStartState()
    {
        currentGameState = GameState.GameStart;
        
        // TODO : 
        //switch (currentStage)
        //{
        //    case GameStage.Stage1:
        //        //LoadScene(stage1)
        //        break;
        //    case GameStage.Stage2:
        //        //LoadScene(stage2)
        //        break;
        //}
        // SoundManager.Instance.PlayBGM("InGameBGM");
    }

    // State : 게임 오버
    public void GameOverState()
    {
        currentGameState = GameState.GameOver;

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameOverBGM");
        // UIManager.Instance.ActiveUI("GameOverUI")
    }

    // State : 게임 클리어
    public void GameClearState()
    {
        currentGameState = GameState.GameClear;

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameClearBGM");
        // UIManager.Instance.ActiveUI("GameClearUI")
    }
}

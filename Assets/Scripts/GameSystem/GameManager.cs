using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Player player;


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

        switch (currentGameState)
        {
            case GameState.Intro:
                IntroState();
                break;
            case GameState.GameStart:
                GameStartState();
                break;
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
        // TODO : 
        // SoundManager.Instance.PlayBGM("InGameBGM");
    }

    // State : 게임 오버
    public void GameOver()
    {
        currentGameState = GameState.GameOver;

        Debug.Log("GameOver");

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameOverBGM");
        // UIManager.Instance.ActiveUI("GameOverUI")
    }

    // State : 게임 클리어
    public void GameClear()
    {
        currentGameState = GameState.GameClear;

        Debug.Log("GameClear");

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameClearBGM");
        // UIManager.Instance.ActiveUI("GameClearUI")
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToIntroScene()
    {
        // TODO : 이름 대신 buildindex로 바꾸기
        currentGameState = GameState.Intro;
        SceneManager.LoadScene("IntroScene");
    }

    public void StageSelect(GameStage newStage)
    {
        currentStage = newStage;
        currentGameState = GameState.GameStart;


        //int buildIndexOfStage1 = ??;  //추후 씬 구성 시 buildIndex 넣기)
        //int newStageIndex = buildIndexOfStage1 + (int)currentStage;
        //SceneManager.LoadScene(newStageIndex);

    }

    public void NextStage()
    {
        // TODO : 현재 스테이지 개수 2개 이므로 간단히 작성한 코드.
        // 스테이지 개수 증가 시 아래 코드로 변경
        currentGameState = GameState.GameStart;
        currentStage = GameStage.Stage2;
        SceneManager.LoadScene("Stage2");

        // TODO : 스테이지 여러개일 경우 코드.
        //int currentIndex = (int)currentStage;
        //int lastStageIndex = GameStage.GetValues(typeof(GameStage)).Length - 1;
        //if( currentIndex == lastStageIndex)
        //{
        //    Debug.Log("현재 마지막 스테이지이므로 다음 스테이지는 없습니다!!");
        //}
        //else
        //{
        //    currentGameState = GameState.GameStart;
        //    currentStage++;
        //    int buildIndexOfStage1 = ??;
        //    int nextStageIndex = buildIndexOfStage1 + currentIndex;
        //    SceneManager.LoadScene(nextStageIndex);
        //}
    }
}

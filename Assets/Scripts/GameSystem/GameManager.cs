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

        switch (currentGameState)
        {
            case GameState.Intro:
                IntroState();
                break;
            case GameState.GameStart:
                GameStartState();
                break;
            default:
                currentGameState = GameState.GameStart;
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

    public void GameStart()
    {
        currentGameState = GameState.GameStart;
        // TODO : 추후 Demo_Scene -> 씬 build index로 수정
        SceneManager.LoadScene("Demo_Scene");
    }

    // State : 게임 오버
    public void GameOver()
    {
        Debug.Log("GameOver");
        currentGameState = GameState.GameOver;
        player.controller.DisablePlayerInput();
        UIManager.Instance.ActiveUI(GameState.GameOver);

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameOverBGM");
    }

    // State : 게임 클리어
    public void GameClear()
    {
        Debug.Log("GameClear");
        currentGameState = GameState.GameClear;
        player.controller.DisablePlayerInput();
        UIManager.Instance.ActiveUI(GameState.GameClear);

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameClearBGM");

    }

    public void Restart()
    {
        Debug.Log("Restart");
        currentGameState = GameState.GameStart;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToIntroScene()
    {
        // TODO : 이름 대신 buildindex로 바꾸기
        currentGameState = GameState.Intro;
        SceneManager.LoadScene("IntroScene");
        Debug.Log("IntroScene!!");
    }

    /* 
     * TODO :
     * 스테이지 1개로 바뀌었으므로 아래 코드들 미사용
     * 추후 리팩토링하여 스테이지 추가로 만들 경우 사용예정
     */
            
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
        // TODO : 스테이지 개수 증가 시 아래 코드로 변경
        currentGameState = GameState.GameStart;
        currentStage = GameStage.Stage2;
        //SceneManager.LoadScene("Stage2");
        Debug.Log("Stage2!!");

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

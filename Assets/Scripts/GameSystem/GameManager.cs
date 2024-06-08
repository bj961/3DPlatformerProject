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

    // State : ����ȭ��
    public void IntroState()
    {
        currentGameState = GameState.Intro;

        // TODO : 
        // LoadScene(IntroScene);
        // SoundManager.Instance.PlayBGM("IntroBGM");
    }

    // State : ���� ����
    public void GameStartState()
    { 
        // TODO : 
        // SoundManager.Instance.PlayBGM("InGameBGM");
    }

    // State : ���� ����
    public void GameOver()
    {
        currentGameState = GameState.GameOver;

        Debug.Log("GameOver");

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameOverBGM");
        // UIManager.Instance.ActiveUI("GameOverUI")
    }

    // State : ���� Ŭ����
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
        // TODO : �̸� ��� buildindex�� �ٲٱ�
        currentGameState = GameState.Intro;
        SceneManager.LoadScene("IntroScene");
    }

    public void StageSelect(GameStage newStage)
    {
        currentStage = newStage;
        currentGameState = GameState.GameStart;


        //int buildIndexOfStage1 = ??;  //���� �� ���� �� buildIndex �ֱ�)
        //int newStageIndex = buildIndexOfStage1 + (int)currentStage;
        //SceneManager.LoadScene(newStageIndex);

    }

    public void NextStage()
    {
        // TODO : ���� �������� ���� 2�� �̹Ƿ� ������ �ۼ��� �ڵ�.
        // �������� ���� ���� �� �Ʒ� �ڵ�� ����
        currentGameState = GameState.GameStart;
        currentStage = GameStage.Stage2;
        SceneManager.LoadScene("Stage2");

        // TODO : �������� �������� ��� �ڵ�.
        //int currentIndex = (int)currentStage;
        //int lastStageIndex = GameStage.GetValues(typeof(GameStage)).Length - 1;
        //if( currentIndex == lastStageIndex)
        //{
        //    Debug.Log("���� ������ ���������̹Ƿ� ���� ���������� �����ϴ�!!");
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

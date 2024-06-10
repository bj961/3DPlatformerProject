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

        //switch (currentGameState)
        //{
        //    case GameState.Intro:
        //        IntroState();
        //        break;
        //    case GameState.GameStart:
        //        GameStartState();
        //        break;
        //    default:
        //        currentGameState = GameState.GameStart;
        //        GameStartState();
        //        break;
        //}
    }

    // State : ����ȭ��
    public void IntroState()
    {
        Debug.Log("IntroScene!!");
        currentGameState = GameState.Intro;
        SceneManager.LoadScene(0);
        // SoundManager.Instance.PlayBGM("IntroBGM");
    }

    // State : ���� ����
    public void GameStartState()
    {
        // TODO : 
        // SoundManager.Instance.PlayBGM("InGameBGM");
    }

    public void GameStart()
    {
        currentGameState = GameState.GameStart;
        SceneManager.LoadScene(1);
    }

    // State : ���� ����
    public void GameOver()
    {
        Debug.Log("GameOver");
        currentGameState = GameState.GameOver;
        player.controller.DisablePlayerInput();
        UIManager.Instance.ActiveUI(GameState.GameOver);

        // TODO : 
        // SoundManager.Instance.PlayBGM("GameOverBGM");
    }

    // State : ���� Ŭ����
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
        // TODO : �̸� ��� buildindex�� �ٲٱ�
        currentGameState = GameState.Intro;
        SceneManager.LoadScene(0);
        Debug.Log("IntroScene!!");
    }

    /* 
     * TODO :
     * �������� 1���� �ٲ�����Ƿ� �Ʒ� �ڵ�� �̻��
     * ���� �����丵�Ͽ� �������� �߰��� ���� ��� ��뿹��
     */

    public void StageSelect(GameStage newStage)
    {
        currentStage = newStage;
        currentGameState = GameState.GameStart;

        int buildIndexOfStage1 = 1;
        int newStageIndex = buildIndexOfStage1 + (int)currentStage;
        SceneManager.LoadScene(newStageIndex);
    }

    public void NextStage()
    {

        Debug.Log("NextStage!");

        int currentIndex = (int)currentStage;
        int lastStageIndex = GameStage.GetValues(typeof(GameStage)).Length - 1;
        if (currentIndex == lastStageIndex)
        {
            Debug.Log("���� ������ ���������̹Ƿ� ���� ���������� �����ϴ�!!");
        }
        else
        {
            currentGameState = GameState.GameStart;
            currentStage++;
            int buildIndexOfStage1 = 1;
            int nextStageIndex = buildIndexOfStage1 + currentIndex;
            SceneManager.LoadScene(nextStageIndex);
        }
    }
}

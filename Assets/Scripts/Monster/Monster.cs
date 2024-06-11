using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform을 할당할 변수
    private NavMeshAgent agent;  // NavMeshAgent 컴포넌트를 할당할 변수

    public float monsterSpeed;  // 몬스터 속도 변수

    private Animator animator;

    private MonsterSound monsterSound; // 몬스터 소리 재생을 위한 MonsterSound 스크립트

    private bool isMoving = false; // 몬스터가 이동 중인지 여부
    private bool hasPlayedInitialSound = false; // 처음 사운드가 재생되었는지 여부

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  // NavMeshAgent 컴포넌트를 가져옴
        agent.speed = monsterSpeed;     // 몬스터 기본 속도 적용
        animator = GetComponent<Animator>();
        monsterSound = GetComponent<MonsterSound>(); // MonsterSound 스크립트를 가져옴
    }

    private void Start()
    {
        // 처음 생성될 때 사운드 파일 1 재생
        monsterSound.PlayInitialSound();
        hasPlayedInitialSound = true;
        SetTarget();
        StartCoroutine(PlayChaseSoundWithDelay(5f)); // 5초 딜레이 후 추적 사운드 재생
    }

    private IEnumerator PlayChaseSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        monsterSound.PlayRandomChaseSound(); // 추적 소리 재생
    }

    void FixedUpdate()
    {
        // TODO : GameState.GameStart에서만 동작하도록 수정
        if (GameManager.Instance.currentGameState == GameState.GameStart)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);  // 플레이어의 위치를 목적지로 설정
                animator.SetBool("isMoving", true);

                if (!isMoving)
                {
                    isMoving = true;
                    if (hasPlayedInitialSound)
                    {
                        monsterSound.PlayRandomChaseSound(); // 추적 소리 재생
                    }
                }
            }
            else
            {
                animator.SetBool("isMoving", false);

                if (isMoving)
                {
                    isMoving = false;
                    monsterSound.StopSound(); // 이동 소리 중지
                }

                SetTarget();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // 충돌 오브젝트 태그 확인
        {
            //SceneManager.LoadScene("Test_uk");  // Scene 재시작

            if (GameManager.Instance.currentGameState == GameState.GameStart)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public void SetTarget()
    {
        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.gameObject.TryGetComponent<Transform>(out player);
        }
    }
}

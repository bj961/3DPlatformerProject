using Unity.Services.Analytics.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform을 할당할 변수
    private NavMeshAgent agent;  // NavMeshAgent 컴포넌트를 할당할 변수

    public float monsterSpeed;  // 몬스터 속도 변수

    private Animator animator;

    private void Awake()
    {
        //player = GameManager.Instance.playerObject.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();  // NavMeshAgent 컴포넌트를 가져옴
        agent.speed = monsterSpeed;     // 몬스터 기본 속도 적용
        //TryGetComponent<Animator>(out animator);
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);  // 플레이어의 위치를 목적지로 설정
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // 충돌 오브젝트 태그 확인
        {
            SceneManager.LoadScene("Test_uk");  // Scene 재시작
            Debug.Log("몬스터-플레이어 충돌");
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform�� �Ҵ��� ����
    private NavMeshAgent agent;  // NavMeshAgent ������Ʈ�� �Ҵ��� ����

    public float monsterSpeed;  // ���� �ӵ� ����

    private Animator animator;

    private MonsterSound monsterSound; // ���� �Ҹ� ����� ���� MonsterSound ��ũ��Ʈ

    private bool isMoving = false; // ���Ͱ� �̵� ������ ����
    private bool hasPlayedInitialSound = false; // ó�� ���尡 ����Ǿ����� ����

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  // NavMeshAgent ������Ʈ�� ������
        agent.speed = monsterSpeed;     // ���� �⺻ �ӵ� ����
        animator = GetComponent<Animator>();
        monsterSound = GetComponent<MonsterSound>(); // MonsterSound ��ũ��Ʈ�� ������
    }

    private void Start()
    {
        // ó�� ������ �� ���� ���� 1 ���
        monsterSound.PlayInitialSound();
        hasPlayedInitialSound = true;
        SetTarget();
        StartCoroutine(PlayChaseSoundWithDelay(5f)); // 5�� ������ �� ���� ���� ���
    }

    private IEnumerator PlayChaseSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        monsterSound.PlayRandomChaseSound(); // ���� �Ҹ� ���
    }

    void FixedUpdate()
    {
        // TODO : GameState.GameStart������ �����ϵ��� ����
        if (GameManager.Instance.currentGameState == GameState.GameStart)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);  // �÷��̾��� ��ġ�� �������� ����
                animator.SetBool("isMoving", true);

                if (!isMoving)
                {
                    isMoving = true;
                    if (hasPlayedInitialSound)
                    {
                        monsterSound.PlayRandomChaseSound(); // ���� �Ҹ� ���
                    }
                }
            }
            else
            {
                animator.SetBool("isMoving", false);

                if (isMoving)
                {
                    isMoving = false;
                    monsterSound.StopSound(); // �̵� �Ҹ� ����
                }

                SetTarget();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // �浹 ������Ʈ �±� Ȯ��
        {
            //SceneManager.LoadScene("Test_uk");  // Scene �����

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

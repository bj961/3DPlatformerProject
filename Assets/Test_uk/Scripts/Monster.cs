using Unity.Services.Analytics.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform�� �Ҵ��� ����
    private NavMeshAgent agent;  // NavMeshAgent ������Ʈ�� �Ҵ��� ����

    public float monsterSpeed;  // ���� �ӵ� ����

    private Animator animator;

    private void Awake()
    {
        //player = GameManager.Instance.playerObject.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();  // NavMeshAgent ������Ʈ�� ������
        agent.speed = monsterSpeed;     // ���� �⺻ �ӵ� ����
        //TryGetComponent<Animator>(out animator);
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);  // �÷��̾��� ��ġ�� �������� ����
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // �浹 ������Ʈ �±� Ȯ��
        {
            SceneManager.LoadScene("Test_uk");  // Scene �����
            Debug.Log("����-�÷��̾� �浹");
        }
    }
}

using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform�� �Ҵ��� ����
    private NavMeshAgent agent;  // NavMeshAgent ������Ʈ�� �Ҵ��� ����

    public float monsterSpeed;  // ���� �ӵ� ����

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  // NavMeshAgent ������Ʈ�� ������
        agent.speed = monsterSpeed;     // ���� �⺻ �ӵ� ����
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);  // �÷��̾��� ��ġ�� �������� ����
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // �浹 ������Ʈ �±� Ȯ��
        {
            SceneManager.LoadScene("Test_uk");  // Scene �����
        }
    }
}

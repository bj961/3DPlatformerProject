using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DamageObject.cs ���ÿ� ���� ������ �ִ� ���� ������Ʈ Ŭ������ �θ� Ŭ����
public class DamageObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            // TODO : ĳ���� ��� ȿ��?

        }
        //if (collision.gameObject.CompareTag("Monster"))
        //{
        //    // ���� ���? ���� �ð����� ����?
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // TODO : ���ӿ���
            //GameManager.Instance.GameOver();
            // ĳ���� ��� ȿ��

        }
        //if (collision.gameObject.CompareTag("Monster"))
        //{
        //    // ���� ���? ���� �ð����� ����?
        //}
    }
}

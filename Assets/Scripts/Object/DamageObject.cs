using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // TODO : 게임오버
            //GameManager.Instance.GameOver();
            // 캐릭터 사망 효과

        }
        //if (collision.gameObject.CompareTag("Monster"))
        //{
        //    // 몬스터 사망? 일정 시간동안 정지?
        //}
    }
}

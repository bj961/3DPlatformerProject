using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DamageObject.cs 가시와 같은 데미지 주는 함정 오브젝트 클래스의 부모 클래스
public class DamageObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            // TODO : 캐릭터 사망 효과?

        }
        //if (collision.gameObject.CompareTag("Monster"))
        //{
        //    // 몬스터 사망? 일정 시간동안 정지?
        //}
    }
}

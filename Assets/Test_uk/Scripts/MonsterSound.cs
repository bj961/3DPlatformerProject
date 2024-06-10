using UnityEngine;
using System.Collections;

public class MonsterSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip initialSound;
    public AudioClip[] chaseSounds; // 추적 시 재생할 사운드 배열

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // 루프를 사용하지 않음
    }

    public void PlayInitialSound()
    {
        if (initialSound != null && audioSource != null)
        {
            audioSource.clip = initialSound;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    public void PlayRandomChaseSound()
    {
        if (chaseSounds.Length > 0 && audioSource != null)
        {
            StartCoroutine(PlayRandomChaseSoundWithDelay());
        }
    }

    private IEnumerator PlayRandomChaseSoundWithDelay()
    {
        while (true) // 무한 루프를 통해 지속적으로 사운드 재생
        {
            yield return new WaitForSeconds(5f); // 5초 딜레이

            if (chaseSounds.Length > 0)
            {
                AudioClip randomClip = chaseSounds[Random.Range(0, chaseSounds.Length)];
                audioSource.clip = randomClip;
                audioSource.loop = false;
                audioSource.Play();
                yield return new WaitForSeconds(randomClip.length); // 사운드 길이만큼 대기
            }
        }
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            StopAllCoroutines(); // 모든 코루틴 중지
        }
    }
}


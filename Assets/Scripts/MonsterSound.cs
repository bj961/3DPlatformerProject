using UnityEngine;
using System.Collections;

public class MonsterSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip initialSound;
    public AudioClip[] chaseSounds; // ���� �� ����� ���� �迭

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // ������ ������� ����
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
        while (true) // ���� ������ ���� ���������� ���� ���
        {
            yield return new WaitForSeconds(5f); // 5�� ������

            if (chaseSounds.Length > 0)
            {
                AudioClip randomClip = chaseSounds[Random.Range(0, chaseSounds.Length)];
                audioSource.clip = randomClip;
                audioSource.loop = false;
                audioSource.Play();
                yield return new WaitForSeconds(randomClip.length); // ���� ���̸�ŭ ���
            }
        }
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            StopAllCoroutines(); // ��� �ڷ�ƾ ����
        }
    }
}


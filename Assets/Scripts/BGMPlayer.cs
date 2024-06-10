using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgmClip;  // ����� ������� ����
    public float volumeLevel = 0.5f; // �ʱ� ���� ���� (0.0f ~ 1.0f)

    private AudioSource audioSource;
    private static BGMPlayer instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;  // �ݺ� ��� ����
        audioSource.playOnAwake = true;  // ���� ���� �� �ڵ� ���
        audioSource.volume = volumeLevel;  // �ʱ� ���� ����

        audioSource.Play();
    }

    // ������ �����ϴ� �޼���
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}

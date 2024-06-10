using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgmClip;  // 재생할 배경음악 파일
    public float volumeLevel = 0.5f; // 초기 볼륨 설정 (0.0f ~ 1.0f)

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
        audioSource.loop = true;  // 반복 재생 설정
        audioSource.playOnAwake = true;  // 게임 시작 시 자동 재생
        audioSource.volume = volumeLevel;  // 초기 볼륨 설정

        audioSource.Play();
    }

    // 볼륨을 설정하는 메서드
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}

using UnityEngine;

public class MonsterAudioController : MonoBehaviour
{
    public AudioSource monsterAudioSource;
    public Transform playerTransform;
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    void Start()
    {
        if (monsterAudioSource == null)
        {
            monsterAudioSource = GetComponent<AudioSource>();
        }
        playerTransform = GameManager.Instance.player.gameObject.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            if (distanceToPlayer <= maxDistance)
            {
                if (!monsterAudioSource.isPlaying)
                {
                    monsterAudioSource.Play();
                }
                float volume = Mathf.Lerp(1.0f, 0.0f, (distanceToPlayer - minDistance) / (maxDistance - minDistance));
                monsterAudioSource.volume = volume;
            }
            else
            {
                if (monsterAudioSource.isPlaying)
                {
                    monsterAudioSource.Stop();
                }
            }
        }
    }
}

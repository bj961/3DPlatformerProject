using UnityEngine;

public class DoorOpenSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}


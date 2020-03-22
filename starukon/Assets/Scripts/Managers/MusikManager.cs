using UnityEngine;

public class MusikManager : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip currentClip;
    public AudioClip[] musiks;

    private void Start()
    {
        audioSource.clip = musiks[Random.Range(0, musiks.Length - 1)];
        currentClip = audioSource.clip;
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            AudioClip clip = null;
            while (currentClip == clip)
            {
                clip = musiks[Random.Range(0, 3)];
            }
            currentClip = audioSource.clip;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

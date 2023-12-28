using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class FootstepSounds : MonoBehaviour
{
    public AudioClip defaultFootstepSound;
    public AudioClip grassFootstepSound;
    public AudioClip concreteFootstepSound;
    // Add more surfaces and corresponding sounds as needed

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set the default footstep sound lolo
        audioSource.clip = defaultFootstepSound;
    }

    public void PlayFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (audioSource != null )
        {
            audioSource.Play();
        }
    }

}

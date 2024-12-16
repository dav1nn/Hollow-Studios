using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundOnClick : MonoBehaviour
{
    public AudioClip clickSound;                 
    public AudioMixerGroup audioMixerGroup;     
    private AudioSource audioSource;

    void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();

        
        if (audioMixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        
        audioSource.playOnAwake = false;       
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    void PlayClickSound()
    {
        
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}


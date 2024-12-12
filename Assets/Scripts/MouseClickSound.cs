using UnityEngine;  

public class MouseClickSound : MonoBehaviour
{
    public AudioClip leftClickSound;  
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
        if (leftClickSound == null)
        {
            Debug.LogError("Left Click Sound is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            PlayLeftClickSound();
        }
    }

    void PlayLeftClickSound()
    {
        if (leftClickSound != null)
        {
            audioSource.PlayOneShot(leftClickSound);
        }
        else
        {
            Debug.LogError("Left click sound is not assigned!");
        }
    }
}

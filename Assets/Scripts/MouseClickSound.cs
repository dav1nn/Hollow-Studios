using UnityEngine;  // This is required for Unity-specific components like AudioSource

public class MouseClickSound : MonoBehaviour
{
    public AudioClip leftClickSound;  // Assign this in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioClip is assigned
        if (leftClickSound == null)
        {
            Debug.LogError("Left Click Sound is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button
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
